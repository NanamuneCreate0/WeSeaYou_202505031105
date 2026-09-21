/*using System.Collections.Generic;
using UnityEngine;

public class MenuExecutor_ActionScene : MonoBehaviour
{
    public enum Tab
    {
        Config,
        Stage,
        Item
    }

    [System.Serializable]
    public class CursorObject
    {
        public Vector2Int position;
        public GameObject obj;
    }

    [System.Serializable]
    public class TabStructure
    {
        public Tab tab;
        public GameObject tabObj;
        public List<CursorObject> cursorObjects;
    }

    [SerializeField] private Tab currentTab = Tab.Config;
    [SerializeField] private List<TabStructure> tabStructures;

    private Vector2Int cursorPosition = Vector2Int.zero;
    private Vector2 previousMove;
    private CursorObject currentCursorObject;

    private Dictionary<GameObject, Vector3> originalScales = new Dictionary<GameObject, Vector3>();

    private void Start()
    {
        foreach (TabStructure tabStructure in tabStructures)
        {
            foreach (CursorObject cursorObject in tabStructure.cursorObjects)
            {
                if (cursorObject.obj != null)
                {
                    originalScales[cursorObject.obj] = cursorObject.obj.transform.localScale;
                }
            }
        }

        UpdateTabObjects();

        foreach (TabStructure tabStructure in tabStructures)
        {
            if (tabStructure.tab != currentTab)
            {
                continue;
            }

            foreach (CursorObject cursorObject in tabStructure.cursorObjects)
            {
                if (cursorObject.position == cursorPosition)
                {
                    currentCursorObject = cursorObject;
                    SetSelectedScale(cursorObject.obj);
                    break;
                }
            }

            break;
        }
    }

    private void Update()
    {
        if (InputManager.Instance.actions.UI_Nana.TabLeft.WasPressedThisFrame())
        {
            ChangeTab(-1);
        }

        if (InputManager.Instance.actions.UI_Nana.TabRight.WasPressedThisFrame())
        {
            ChangeTab(1);
        }

        Vector2 move = InputManager.Instance.actions.UI_Nana.Move.ReadValue<Vector2>(); // Mode DigitalNormalized

        if (move != Vector2.zero && previousMove == Vector2.zero)
        {
            MoveCursor(move);
        }

        previousMove = move;
    }

    private void ChangeTab(int direction)
    {
        if (currentCursorObject != null)
        {
            ResetScale(currentCursorObject.obj);
            currentCursorObject = null;
        }

        int tabCount = System.Enum.GetValues(typeof(Tab)).Length;

        int nextTab = (int)currentTab + direction;

        if (nextTab < 0)
        {
            nextTab = tabCount - 1;
        }
        else if (nextTab >= tabCount)
        {
            nextTab = 0;
        }

        currentTab = (Tab)nextTab;

        cursorPosition = Vector2Int.zero;

        UpdateTabObjects();

        foreach (TabStructure tabStructure in tabStructures)
        {
            if (tabStructure.tab != currentTab)
            {
                continue;
            }

            foreach (CursorObject cursorObject in tabStructure.cursorObjects)
            {
                if (cursorObject.position == cursorPosition)
                {
                    currentCursorObject = cursorObject;
                    SetSelectedScale(cursorObject.obj);
                    break;
                }
            }

            break;
        }

        Debug.Log($"Tab: {currentTab} / Cursor: {cursorPosition}");
    }

    private void UpdateTabObjects()
    {
        foreach (TabStructure tabStructure in tabStructures)
        {
            if (tabStructure.tabObj != null)
            {
                tabStructure.tabObj.SetActive(tabStructure.tab == currentTab);
            }
        }
    }

    private void MoveCursor(Vector2 move)
    {
        Vector2Int direction = Vector2Int.RoundToInt(move);

        Vector2Int nextPosition = cursorPosition + direction;

        foreach (TabStructure tabStructure in tabStructures)
        {
            if (tabStructure.tab != currentTab)
            {
                continue;
            }

            foreach (CursorObject cursorObject in tabStructure.cursorObjects)
            {
                if (cursorObject.position == nextPosition)
                {
                    if (currentCursorObject != null)
                    {
                        ResetScale(currentCursorObject.obj);
                    }

                    cursorPosition = nextPosition;
                    currentCursorObject = cursorObject;

                    SetSelectedScale(currentCursorObject.obj);

                    Debug.Log($"Tab: {currentTab} / Cursor: {cursorPosition}");

                    return;
                }
            }

            break;
        }

        Debug.Log($"Cursor移動キャンセル: {nextPosition}");
    }

    private void SetSelectedScale(GameObject obj)
    {
        if (obj == null)
        {
            return;
        }

        if (originalScales.ContainsKey(obj))
        {
            obj.transform.localScale = originalScales[obj] * 1.1f;
        }
    }

    private void ResetScale(GameObject obj)
    {
        if (obj == null)
        {
            return;
        }

        if (originalScales.ContainsKey(obj))
        {
            obj.transform.localScale = originalScales[obj];
        }
    }
}*/
using System.Collections.Generic;
using UnityEngine;

public class MenuExecutor_ActionScene : MonoBehaviour
{
    public enum Tab
    {
        Item,
        Config,
        Reset
    }

    [System.Serializable]
    public class CursorObject
    {
        public Vector2Int position;
        public GameObject obj;
        public bool executeInsteadOfMove;
        public bool executeOnDecide;
    }

    [System.Serializable]
    public class TabStructure
    {
        public Tab tab;
        public GameObject tabObj;
        public List<CursorObject> cursorObjects;
    }

    [SerializeField] private Tab currentTab = Tab.Config;
    [SerializeField] private List<TabStructure> tabStructures;

    private Vector2Int cursorPosition = Vector2Int.zero;
    private Vector2 previousMove;
    private CursorObject currentCursorObject;

    private Dictionary<GameObject, Vector3> originalScales = new Dictionary<GameObject, Vector3>();

    private void Start()
    {
        foreach (TabStructure tabStructure in tabStructures)
        {
            foreach (CursorObject cursorObject in tabStructure.cursorObjects)
            {
                if (cursorObject.obj != null)
                {
                    originalScales[cursorObject.obj] = cursorObject.obj.transform.localScale;
                }
            }
        }

        UpdateTabObjects();

        foreach (TabStructure tabStructure in tabStructures)
        {
            if (tabStructure.tab != currentTab)
            {
                continue;
            }

            foreach (CursorObject cursorObject in tabStructure.cursorObjects)
            {
                if (cursorObject.position == cursorPosition)
                {
                    currentCursorObject = cursorObject;
                    SetSelectedScale(cursorObject.obj);
                    break;
                }
            }

            break;
        }
    }

    private void Update()
    {
        if (InputManager.Instance.actions.UI_Nana.TabLeft.WasPressedThisFrame())
        {
            ChangeTab(-1);
        }

        if (InputManager.Instance.actions.UI_Nana.TabRight.WasPressedThisFrame())
        {
            ChangeTab(1);
        }

        if (InputManager.Instance.actions.UI_Nana.Decide.WasPressedThisFrame())
        {
            ExecuteCurrentCursorFunction();
        }

        Vector2 move = InputManager.Instance.actions.UI_Nana.Move.ReadValue<Vector2>(); // Mode DigitalNormalized

        if (move != Vector2.zero && previousMove == Vector2.zero)
        {
            MoveCursor(move);
        }

        previousMove = move;
    }

    private void ChangeTab(int direction)
    {
        if (currentCursorObject != null)
        {
            ResetScale(currentCursorObject.obj);
            currentCursorObject = null;
        }

        int tabCount = System.Enum.GetValues(typeof(Tab)).Length;

        int nextTab = (int)currentTab + direction;

        if (nextTab < 0)
        {
            nextTab = tabCount - 1;
        }
        else if (nextTab >= tabCount)
        {
            nextTab = 0;
        }

        currentTab = (Tab)nextTab;

        cursorPosition = Vector2Int.zero;

        UpdateTabObjects();

        foreach (TabStructure tabStructure in tabStructures)
        {
            if (tabStructure.tab != currentTab)
            {
                continue;
            }

            foreach (CursorObject cursorObject in tabStructure.cursorObjects)
            {
                if (cursorObject.position == cursorPosition)
                {
                    currentCursorObject = cursorObject;
                    SetSelectedScale(cursorObject.obj);
                    break;
                }
            }

            break;
        }

        Debug.Log($"Tab: {currentTab} / Cursor: {cursorPosition}");
    }

    private void UpdateTabObjects()
    {
        foreach (TabStructure tabStructure in tabStructures)
        {
            if (tabStructure.tabObj != null)
            {
                tabStructure.tabObj.SetActive(tabStructure.tab == currentTab);
            }
        }
    }

    private void MoveCursor(Vector2 move)
    {
        Vector2Int direction = Vector2Int.RoundToInt(move);

        Vector2Int nextPosition = cursorPosition + direction;

        foreach (TabStructure tabStructure in tabStructures)
        {
            if (tabStructure.tab != currentTab)
            {
                continue;
            }

            foreach (CursorObject cursorObject in tabStructure.cursorObjects)
            {
                if (cursorObject.position == nextPosition)
                {
                    if (cursorObject.executeInsteadOfMove)
                    {
                        ExecuteFunction(cursorObject);
                        return;
                    }

                    if (currentCursorObject != null)
                    {
                        ResetScale(currentCursorObject.obj);
                    }

                    cursorPosition = nextPosition;
                    currentCursorObject = cursorObject;

                    SetSelectedScale(currentCursorObject.obj);

                    Debug.Log($"Tab: {currentTab} / Cursor: {cursorPosition}");

                    return;
                }
            }

            break;
        }

        Debug.Log($"Cursor移動キャンセル: {nextPosition}");
    }

    private void ExecuteCurrentCursorFunction()
    {
        if (currentCursorObject == null)
        {
            return;
        }

        if (!currentCursorObject.executeOnDecide)
        {
            return;
        }

        ExecuteFunction(currentCursorObject);
    }

    private void ExecuteFunction(CursorObject cursorObject)
    {
        IMenuFunction function = cursorObject.obj?.GetComponent<IMenuFunction>();

        if (function != null)
        {
            function.Execute();
        }
    }

    private void SetSelectedScale(GameObject obj)
    {
        if (obj == null)
        {
            return;
        }

        if (originalScales.ContainsKey(obj))
        {
            obj.transform.localScale = originalScales[obj] * 1.1f;
        }
    }

    private void ResetScale(GameObject obj)
    {
        if (obj == null)
        {
            return;
        }

        if (originalScales.ContainsKey(obj))
        {
            obj.transform.localScale = originalScales[obj];
        }
    }
}

/*
特に ChangeTab() は、

Scaleを戻す
→ currentTab変更
→ cursorPositionを(0,0)
→ UpdateTabObjects()
→ 新しいTabの (0,0) を選択

という順番
*/