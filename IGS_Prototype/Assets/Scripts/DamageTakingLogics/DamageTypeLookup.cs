using System.Collections.Generic;
using UnityEngine;

public class DamageTypeLookup
{
    public class DamageTypeRecord
    {
        public DamageType damageType;
        public float multiplier;

        public DamageTypeRecord(DamageType damageType, float multiplier)
        {
            this.damageType = damageType;
            this.multiplier = multiplier;
        }
    };
    
    public static Dictionary<DamageType, DamageTypeRecord[]> affinityTable = new ()
    {
        { DamageType.Dark, new []
            {
                new DamageTypeRecord(DamageType.Holy, 1.5f),
                new DamageTypeRecord(DamageType.Fire, 1.2f),
            }
        },
        { DamageType.Holy, new []
            {
                new DamageTypeRecord(DamageType.Frost, 1.5f),
                new DamageTypeRecord(DamageType.Dark, 1.2f),
            }
        },
        { DamageType.Fire, new []
            {
                new DamageTypeRecord(DamageType.Frost, 1.5f),
                new DamageTypeRecord(DamageType.Fire, 1.5f),
            }
        },
        { DamageType.Frost, new []
            {
                new DamageTypeRecord(DamageType.Fire, 1.5f),
                new DamageTypeRecord(DamageType.Dark, 1.2f),
            }
        }
    };
    
    public static Dictionary<DamageType, DamageTypeRecord[]> weaknessTable = new ()
    {
        { DamageType.Dark, new []
            {
                new DamageTypeRecord(DamageType.Frost, 0.9f),
                new DamageTypeRecord(DamageType.Dark, 0.5f),
            }
        },
        { DamageType.Holy, new []
            {
                new DamageTypeRecord(DamageType.Fire, 0.9f),
                new DamageTypeRecord(DamageType.Holy, 0.5f),
            }
        },
        { DamageType.Fire, new []
            {
                new DamageTypeRecord(DamageType.Dark, 0.7f),
            }
        },
        { DamageType.Frost, new []
            {
                new DamageTypeRecord(DamageType.Frost, 0.3f),
                new DamageTypeRecord(DamageType.Holy, 0.7f),
            }
        },
    };

    public static Dictionary<DamageType, Color> colorTable = new ()
    {
        { DamageType.Dark, new Color(0.2f, 0f, 0.2f) },
        { DamageType.Holy, new Color(0.9f, 0.9f, 0.6f) },
        { DamageType.Fire, new Color(0.9f, 0.6f, 0.2f) },
        { DamageType.Frost, new Color(0.5f, 0.6f, 0.9f) },
    };
}