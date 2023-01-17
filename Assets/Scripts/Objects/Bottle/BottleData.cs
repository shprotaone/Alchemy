using System.Collections.Generic;

public class BottleData
{    
    private List<PotionLabelType> _labels;
    private Potion _potionInBottle;

    private bool _isFull;

    public Potion PotionInBottle => _potionInBottle;
    public List<PotionLabelType> Labels => _labels;
    public bool IsFull => _isFull;

    public BottleData(Potion potion, bool isFull)
    {
        _labels = new List<PotionLabelType>();
        _labels.AddRange(potion.Labels);
        _labels.Sort();

        _isFull = isFull;
        _potionInBottle = potion;
    }
}
