using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private float currentExp;
    [SerializeField] private int level;
    [SerializeField] private int attack;
    [SerializeField] private int defend;

    public int[] _levels = new int[100];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // khởi tạo giá trị cho mảng _levels
        // level 1 lên level 2 cần 100exp
        // level sau cần thêm 10% exp so vs level trc đó
        _levels[0] = 100;
        for (int i = 1; i < 100; i++)
        {
            _levels[i] = _levels[i - 1] + (int)(_levels[i - 1] * 0.1f);
        }
        // khởi tạo giá trị cho player info
        level = 1;
        currentExp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            AddExp(20);
        }
        
    }

    private void AddExp(float exp)
    {
        currentExp += exp;

        // nếu exp hiện tại lớn hơn hoặc bằng exp cần để lên level
        if (currentExp >= _levels[level - 1])
        {
            // giảm exp hiện tại đi exp cần để lên level
            currentExp -= _levels[level - 1];
            // tăng level
            level++;
            // tăng chỉ số
            attack += 10;
            defend += 5;
        }

    }
}
