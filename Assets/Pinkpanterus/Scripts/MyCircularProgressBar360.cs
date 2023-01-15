using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyCircularProgressBar360 : MonoBehaviour 
{
	[Header("Colors")]
	//[SerializeField] private Color m_MainColor = Color.white;
	[SerializeField] private Color m_FillColor = Color.green;
	
	[Header("General")]
	private int m_NumberOfSegments = 1;
	[Range(0, 360)] [SerializeField] private float m_StartAngle = 40;
	[Range(0, 360)] [SerializeField] private float m_EndAngle = 320;
	[SerializeField] private float m_SizeOfNotch = 0;
	[Range(0, 1f)] [SerializeField] private float m_FillAmount = 0.0f;

	[SerializeField] private Transform _segmentParent;

	private Image m_Image;
	private List<Image> m_ProgressToFill = new List<Image>();
	private float m_SizeOfSegment;
	private bool isInit = false;

	public List<Image> ProgressBarParts => m_ProgressToFill;

	public float FillAmount
	{
		get => m_FillAmount;
		set => m_FillAmount = value;
	}

	public int NumberOfSegment
	{
		get => m_NumberOfSegments;
		set => m_NumberOfSegments = value;
	}

	public void SetUpProgressBar(int numberOfSegments, Color[] colorsOfSegments) 
	{
        // Get images in Children
        if (!isInit)
        {
			m_Image = GetComponentInChildren<Image>();
			m_Image.gameObject.SetActive(false);
			m_NumberOfSegments = numberOfSegments;
			
			// Calculate notches
			float startNormalAngle = NormalizeAngle(m_StartAngle);
			float endNormalAngle = NormalizeAngle(360 - m_EndAngle);
			float notchesNormalAngle = (m_NumberOfSegments - 1) * NormalizeAngle(m_SizeOfNotch);
			float allSegmentsAngleArea = 1 - startNormalAngle - endNormalAngle - notchesNormalAngle;

			// Count size of segments
			m_SizeOfSegment = allSegmentsAngleArea / m_NumberOfSegments;
			for (int i = 0; i < m_NumberOfSegments; i++)
			{
				GameObject currentSegment = Instantiate(m_Image.gameObject, transform.position, Quaternion.identity, _segmentParent);
				currentSegment.SetActive(true);
			}

			isInit = true;
		}
		var colors = colorsOfSegments;

		ChangeColor(colors);

		StartCoroutine(UpdateProgressbar());
	}

	public void ChangeColor(Color [] colorsOfSegments)
    {
		var colors = colorsOfSegments;

		for (int i = 0; i < m_NumberOfSegments; i++)
        {
			Image segmentImage = _segmentParent.transform.GetChild(i).GetComponent<Image>();
			segmentImage.color = colors[i];
			segmentImage.fillAmount = m_SizeOfSegment;

			Image segmentFillImage = segmentImage.transform.GetChild(0).GetComponent<Image>();
			segmentFillImage.color = m_FillColor;
			m_ProgressToFill.Add(segmentFillImage);

			float zRot = m_StartAngle + i * ConvertCircleFragmentToAngle(m_SizeOfSegment) + i * m_SizeOfNotch;
			segmentImage.transform.rotation = Quaternion.Euler(0, 0, -zRot);
		}
    }
	

	private IEnumerator UpdateProgressbar() 
	{
		
		while (true)
		{
			for (int i = 0; i < m_NumberOfSegments; i++) 
			{
				m_ProgressToFill[i].fillAmount = (m_FillAmount * ((m_EndAngle-m_StartAngle)/360)) - m_SizeOfSegment * i;				
			}

			yield return null;
		}
	}

	private float NormalizeAngle(float angle) 
	{
		return Mathf.Clamp01(angle / 360f);
	}

	private float ConvertCircleFragmentToAngle(float fragment) 
	{
		return 360 * fragment;
	}
}