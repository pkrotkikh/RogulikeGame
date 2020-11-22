using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("STATS")]
    public Race race;
    public Dictionary<Item.Stats, int> playerStats;

    [Header("MOVEMENT")]
    public Tile tile;
    private bool isInputEnabled = true;
    private Rigidbody2D rb;
    private Vector2 pos;

    [HideInInspector]public Vector2[] directions = new Vector2[] {
        Vector2.right, Vector2.right + Vector2.up, Vector2.up, Vector2.left + Vector2.up ,Vector2.left, Vector2.left + Vector2.down, Vector2.down, Vector2.down + Vector2.right };

    private KeyCode[] keys = new KeyCode[] {
        KeyCode.RightArrow, KeyCode.U, KeyCode.UpArrow, KeyCode.Y, KeyCode.LeftArrow, KeyCode.B, KeyCode.DownArrow, KeyCode.N };


    [Header("INVENTORY")]
    [HideInInspector] public Inventory inventory;
    public UI_Inventory uiInventory;
    public UI_GroundItems uiGroundItems;

    [Header("EQUIPMENT")]
    public Equipment equipment;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory = gameObject.GetComponent<Inventory>();
        equipment = gameObject.GetComponent<Equipment>();
        StatsInitialize();

        //Inventory
        uiInventory.SetPlayer(this);
        uiInventory.SetInventory(inventory);
        uiGroundItems.SetPlayer(this);
    }

    void Update()
    {
        Move();
    }

    public void RefreshStats()
    {
        //Сбросить предыдущий перерасчёт статов
        StatsInitialize();

        //Собрать статы с расы


        //Собрать статы с предметов инвентаря
        foreach (KeyValuePair<Item.ItemType, Item> item in equipment.itemList)
        {
            if(item.Value != null)
            {
                foreach (KeyValuePair<Item.Stats, int> stat in item.Value.stats)
                {
                    playerStats[stat.Key] += stat.Value;
                }
            }
        }

        //Учесть влияние статов
        playerStats[Item.Stats.MinDamage] += playerStats[Item.Stats.Strength];
        playerStats[Item.Stats.MaxDamage] += playerStats[Item.Stats.Strength];

        foreach(KeyValuePair<Item.Stats, int> playerStat in playerStats)
        {
            Debug.Log(playerStat.Value);
        }
    }

    void StatsInitialize()
    {
        playerStats = new Dictionary<Item.Stats, int>();

        playerStats.Add(Item.Stats.Strength, 0);
        playerStats.Add(Item.Stats.Dexterity, 0);
        playerStats.Add(Item.Stats.Intelligence, 0);
        playerStats.Add(Item.Stats.Armour, 0);
        playerStats.Add(Item.Stats.MinDamage, 0);
        playerStats.Add(Item.Stats.MaxDamage, 0);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private int GetAttack()
    {
        int attack = Mathf.RoundToInt(Random.Range(playerStats[Item.Stats.MinDamage],playerStats[Item.Stats.MaxDamage]));
        return attack;
    }

    public List<ItemWorld> GetItemsGround()
    {
        return tile.items;
    }

    private void Move()
    {
        // Перебор кнопок перемещения, которые может нажать игрок
        for (int i = 0; i < keys.Length; i++)
        {
            if (Input.GetKey(keys[i]) && isInputEnabled)
            {
                pos = transform.position;
                RaycastHit2D raycastGround = Physics2D.Raycast(pos + directions[i], directions[i], 1, 1 << LayerMask.NameToLayer("Ground"));
                
                if (raycastGround.collider != null)
                {
                    Tile nextTile = raycastGround.transform.GetComponent<Tile>();
                    GameObject opponent = nextTile.creature;
                    if (opponent != null)
                    {
                        Enemy enemy = opponent.GetComponent<Enemy>();
                        Debug.Log("You hit " + enemy.name);
                        StartCoroutine(SmoothAttack(enemy));
                    }
                    else
                    {
                        tile.creature = null;
                        tile = nextTile.GetComponent<Tile>();
                        tile.creature = gameObject;
                        Vector2 endPoint = nextTile.transform.position;
                        StartCoroutine(SmoothMove(endPoint));
                        uiGroundItems.RefreshGroundItems(GetItemsGround());
                    }
                }
            }
        }
    }

    private IEnumerator SmoothAttack(Enemy enemy)
    {
        isInputEnabled = false;
        float smoothMoveTime = 0f;
        float smoothMoveDuration = 0.25f;

        while (smoothMoveTime < smoothMoveDuration)
        {
            smoothMoveTime += Time.deltaTime;
            yield return null;
        }

        if (enemy != null)
        {
            enemy.TakeDamage(GetAttack());
        }

        isInputEnabled = true;
    }

    public IEnumerator SmoothMove(Vector2 endPoint)
    {
        isInputEnabled = false;
        float smoothMoveTime = 0f;
        float smoothMoveDuration = 0.25f;
        Vector2 startPoint = transform.position;

        while (smoothMoveTime < smoothMoveDuration)
        {
            smoothMoveTime += Time.deltaTime;
            gameObject.transform.position = Vector2.Lerp(startPoint, endPoint, smoothMoveTime / smoothMoveDuration);
            yield return null;
        }

        transform.position = endPoint;
        isInputEnabled = true;
    }
}
