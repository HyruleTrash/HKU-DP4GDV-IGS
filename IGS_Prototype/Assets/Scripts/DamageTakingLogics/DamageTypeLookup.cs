using System.Collections.Generic;

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
    
    public static Dictionary<DamageType, DamageTypeRecord[]> affinityTable = new Dictionary<DamageType, DamageTypeRecord[]>()
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
    
    public static Dictionary<DamageType, DamageTypeRecord[]> weaknessTable = new Dictionary<DamageType, DamageTypeRecord[]>()
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
}