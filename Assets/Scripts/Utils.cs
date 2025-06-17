using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class Utils
{
    static public void AddButtonClickListener(Transform parentTransform, string buttonName, UnityAction listener)
    {
        Button playButton = parentTransform.Find(buttonName).GetComponent<Button>();
        playButton.onClick.AddListener(listener);
    }


    /// <summary>
    /// Includes aInt1 and aInt2
    /// </summary>    
    static public int GetRandomIntBetween(int int1, int int2)
    {
        

        return int1 + (int)Mathf.Floor(UnityEngine.Random.value * (int2 - int1 + 1));
    }


    static public T GetRandomListElement<T>(List<T> list)
    {
        return list[GetRandomIntBetween(0, list.Count - 1)];
    }


    static public T GetRandomArrayElement<T>(T[] array)
    {
        return array[GetRandomIntBetween(0, array.Length - 1)];
    }


    static public float GetRandomFloatBetween(float float1, float float2)
    {
        return float1 + UnityEngine.Random.value * (float2 - float1);
    }


    static public float GetSpriteHeight(Transform transform)
    {
        return transform.GetComponent<SpriteRenderer>().bounds.size.y;
    }


    static public float GetSpriteWidth(Transform transform)
    {
        return transform.GetComponent<SpriteRenderer>().bounds.size.x;
    }


    static public float GetScreenBottom()
    {
        return Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;
    }


    static public float GetScreenTop()
    {
        return Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
    }


    static public void SetVelocityX(Rigidbody2D rigidbody, float x)
    {
        Vector2 velocity = rigidbody.linearVelocity;
        velocity.x = x;
        rigidbody.linearVelocity = velocity;
    }


    static public void SetVelocityY(Rigidbody2D rigidbody, float y)
    {
        Vector2 velocity = rigidbody.linearVelocity;
        velocity.y = y;
        rigidbody.linearVelocity = velocity;
    }


    static public void SetPositionX(Transform transform, float x)
    {
        Vector2 position = transform.position;
        position.x = x;
        transform.position = position;
    }


    static public void SetPositionY(Transform transform, float y)
    {
        Vector2 position = transform.position;
        position.y = y;
        transform.position = position;
    }


    static public bool FuzzyEquals(float a, float b, float fuzziness = 0.01f)
    {
        return Mathf.Abs(a - b) < fuzziness;
    }


    static public TextMeshProUGUI GetText(Transform transform, string path)
    {
        return Find(transform, path).GetComponent<TextMeshProUGUI>();
    }

    static public Button FindButton(string path)
    {
        return Find(path).GetComponent<Button>();
    }

    static public Button FindButton(Transform transform, string path)
    {
        return Find(transform, path).GetComponent<Button>();
    }

    static public Transform Find(Transform transform, string path)
    {
        string[] names = path.Split('/');

        Transform currentTransform = transform;

        foreach (string name in names)
        {
            currentTransform = FindChildByName(currentTransform, (name));
            if (currentTransform == null)
            {
                throw new UnityException("Can't find " + name + " in path " + path + " in transform " + transform.name);
            }
        }

        return currentTransform;
    }


    static public Transform FindChildByName(Transform transform, string name)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.name == name)
            {
                return child;
            }
        }

        throw new UnityException("Couldn't find child " + name + " in transform " + transform.name);
    }


    static public void SetText(Transform transform, string path, string text)
    {
        Find(transform, path).GetComponent<TextMeshProUGUI>().text = text;
    }


    static public T GetEnumFromString<T>(string aString)
    {
        return (T)System.Enum.Parse(typeof(T), aString);
    }


    static public Array GetEnumArray<T>()
    {
        return Enum.GetValues(typeof(T));
    }


    static public List<T> GetEnumList<T>()
    {
        Array array = GetEnumArray<T>();
        List<T> list = new List<T>();

        foreach (T element in array)
        {
            list.Add(element);
        }

        return list;
    }


    static public T GetEnumFromInt<T>(int aInt)
    {
        return GetEnumList<T>()[aInt];
    }

    static public void ClampZ(Transform transform, float minZ, float maxZ)
    {
        if (transform.position.z < minZ)
        {
            Vector3 pos = transform.position;
            pos.z = maxZ;
            transform.position = pos;
        }
        else if (transform.position.z > maxZ)
        {
            Vector3 pos = transform.position;
            pos.z = maxZ;
            transform.position = pos;
        }
    }

    static public int RoundToInt(float aFloat)
    {
        return Mathf.RoundToInt(aFloat);
    }

    static public int CeilToInt(float aFloat)
    {
        return Mathf.CeilToInt(aFloat);
    }

    public static float GetScreenSpaceDistance(float worldDistance)
    {
        // Converts worldDistance to a screen space (camera) distance
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found");
            return 0;
        }

        Vector3 worldPoint1 = new Vector3(0, 0, 0);
        Vector3 worldPoint2 = new Vector3(worldDistance, 0, 0);

        Vector3 screenPoint1 = mainCamera.WorldToScreenPoint(worldPoint1);
        Vector3 screenPoint2 = mainCamera.WorldToScreenPoint(worldPoint2);

        return Vector3.Distance(screenPoint1, screenPoint2);
    }

    public static void ResetAllFields(object obj)
    {
        if (obj == null) return;

        // Get all fields from the object's type and its base types
        var fields = obj.GetType().GetFields(
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance
        );

        foreach (var field in fields)
        {
            // Skip readonly fields
            if (field.IsInitOnly) continue;

            // Get default value for the field's type
            object defaultValue = field.FieldType.IsValueType
                ? System.Activator.CreateInstance(field.FieldType)
                : null;

            // Set the field to its default value
            field.SetValue(obj, defaultValue);
        }
    }

    public static int CeilToMultiple(float number, int multipleOf)
    {
        return CeilToInt(number / multipleOf) * multipleOf;
    }

    //Assumes "button" has a child named Text that has a TextMeshProUGUI component.
    static public TextMeshProUGUI GetButtonText(Button button)
    {
        Transform textTransform = button.transform.Find("Text (TMP)");
        if (textTransform == null)
        {
            throw new UnityException("No child Text in button " + button.name);
        }

        TextMeshProUGUI text = textTransform.GetComponent<TextMeshProUGUI>();
        if (text == null)
        {
            throw new UnityException("Text object " + text.name + " doesn't have a TextMeshProUGUI component!");
        }

        return text;
    }

    static public void SetButtonText(Button button, string text)
    {
        GetButtonText(button).text = text;
    }

    internal static bool IsMouseOverUiObject(string uiObjectName)
    {
        if (GameObject.Find(uiObjectName) == null)
        {
            return false;
        }
        else
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {                
                if (EventSystem.current.currentSelectedGameObject == null)
                {                    
                    return false;
                }
                else
                {                    
                    return EventSystem.current.currentSelectedGameObject.name == uiObjectName;
                }
            }            
            else
            {
                return false;
            }
        }        
    }

    static public bool IsMouseOverUiButton()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.GetComponent<Button>() != null)
            {
                return true;
            }
        }

        return false;
    }

    internal static bool GetRandomBoolWeighted(float trueProbability)
    {
        float randomFloat = GetRandomFloatBetween(0, 1);
        return randomFloat < trueProbability;
    }

    public struct WeightedInt
    {
        public int value;
        public float weight;
    }

    internal static int GetRandomIntWeighted(List<WeightedInt> weightedInts)
    {
        float totalWeight = 0;
        foreach (WeightedInt weightedInt in weightedInts)
        {
            totalWeight += weightedInt.weight;
        }

        float randomFloat = GetRandomFloatBetween(0, totalWeight);

        float currentWeight = 0;
        foreach (WeightedInt weightedInt in weightedInts)
        {
            currentWeight += weightedInt.weight;
            if (randomFloat < currentWeight)
            {
                return weightedInt.value;
            }
        }

        throw new UnityException("Couldn't get a random weighted int");     
    }

    internal static T TryFind<T>() where T : UnityEngine.Object
    {
        try
        {
            return Find<T>();
        }
        catch (UnityException)
        {
            return null;
        }
    }

    internal static T Find<T>() where T : UnityEngine.Object
    {
        T theObject = GameObject.FindFirstObjectByType<T>(FindObjectsInactive.Include);
        /*
        if (theObject == null)
        {
            throw new UnityException("Couldn't find object");
        }
        */
        return theObject;
    }

    public static List<T> FindAllList<T>() where T : UnityEngine.Object
    {
        var objects = Resources.FindObjectsOfTypeAll<T>();
        return new List<T>(objects);
    }

    public static T[] FindAllArray<T>() where T : UnityEngine.Object
    {
        var objects = Resources.FindObjectsOfTypeAll<T>();
        return objects;
    }

    internal static GameObject TryFind(string name)
    {
        try
        {
            return Find(name);
        }
        catch (UnityException)
        {
            return null;
        }
    }

    internal static GameObject Find(string name)
    {
        var objects = Resources.FindObjectsOfTypeAll<GameObject>();
        var firstOrDefault = objects.FirstOrDefault(x => x.name == name);
        if (firstOrDefault == null)
        {
            throw new UnityException("No GameObject with name " + name);
        }
        return firstOrDefault;
    }

    internal static T Find<T>(string name) where T : MonoBehaviour
    {
        return FindAllList<T>().Find(x => x.name == name);
    }

    internal static Vector3 GetWorldCenterOfScreen()
    {        
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, Camera.main.nearClipPlane);
        return Camera.main.ScreenToWorldPoint(screenCenter);
    }

    public static List<T> SelectRandomItemsFromList<T>(List<T> sourceList, int itemCount)
    {
        // Check if the input parameters are valid
        if (sourceList == null)
            throw new ArgumentNullException(nameof(sourceList), "Source list cannot be null");

        if (itemCount < 0)
            throw new ArgumentException("Item count cannot be negative", nameof(itemCount));

        if (itemCount > sourceList.Count)
            throw new ArgumentException("Item count cannot exceed source list length", nameof(itemCount));

        // Use Random class to shuffle and select items
        System.Random random = new System.Random();

        // Order by random value and take specified count
        return sourceList
            .OrderBy(x => random.Next())
            .Take(itemCount)
            .ToList();
    }

    static public Vector2 GetAnchoredPosition(this Transform transform)
    {
        return transform.GetComponent<RectTransform>().anchoredPosition;
    }

    static public void SetAnchoredPosition(this Transform transform, Vector2 anchoredPosition)
    {
        transform.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
    }

    static public void SetAnchoredX(this Transform transform, float x)
    {
        SetAnchoredPosition(transform, new Vector2(x, GetAnchoredPosition(transform).y));
    }

    static public float GetAnchoredX(this Transform transform)
    {
        return GetAnchoredPosition(transform).x;
    }

}
