using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneUI : MonoBehaviour
{
    public Text geneNameText;
    public Text geneEffectText;
    public Text geneCostText;
    public Text geneFamilyText;
    public Text genePartText;
    public Text geneTypeText;
    public Button acceptGene;
    public void Create(Gene gene)
    {
        geneNameText.text = gene.name;
        geneEffectText.text = gene.effect;
        geneCostText.text = "Cost: " + gene.cost;
        geneFamilyText.text = gene.GeneFamily;
        genePartText.text = gene.PartToEffect;
        geneTypeText.text = gene.GeneType;
    }
}
