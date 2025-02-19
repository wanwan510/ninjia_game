using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestureRecognizer : MonoBehaviour
{
    public Texture2D drawingTexture;
    private Color drawColor = Color.black;
    private int brushSize = 10;

    void Start()
    {
        // 初始化绘图纹理
        drawingTexture = new Texture2D(Screen.width, Screen.height);
        GetComponent<RawImage>().texture = drawingTexture;
        ClearDrawing();
    }

    void Update()
    {
        // 检测触摸输入
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = touch.position;

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                Draw(touchPos);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // 在觸摸結束時繪製圖案
                DrawShape(touchPos);
            }
        }
    }

    void Draw(Vector2 position)
    {
        // 将触摸位置转换为纹理坐标
        int x = (int)(position.x);
        int y = (int)(position.y);
        
        // 在纹理上绘制
        for (int i = -brushSize; i <= brushSize; i++)
        {
            for (int j = -brushSize; j <= brushSize; j++)
            {
                if (i * i + j * j <= brushSize * brushSize)
                {
                    int px = x + i;
                    int py = y + j;

                    if (px >= 0 && px < drawingTexture.width && py >= 0 && py < drawingTexture.height)
                    {
                        drawingTexture.SetPixel(px, py, drawColor);
                    }
                }
            }
        }
        drawingTexture.Apply();
    }

    void DrawShape(Vector2 position)
    {
        // 在觸摸結束時繪製特定圖案
        int x = (int)(position.x);
        int y = (int)(position.y);

        // 繪製不同的圖案
        string shape = GetShape(); // 這裡可以根據需求獲取要繪製的圖案

        switch (shape)
        {
            case "|":
                DrawLine(x, y, 0, 10); // 垂直線
                break;
            case "upArrow":
                DrawArrow(x, y, true); // 上箭頭
                break;
            case "downArrow":
                DrawArrow(x, y, false); // 下箭頭
                break;
            case "irregular":
                DrawIrregularShape(x, y); // 不規則形狀
                break;
            case "heart":
                DrawHeart(x, y); // 繪製心形
                break;
        }
    }

    void DrawLine(int x, int y, int dx, int length)
    {
        for (int i = 0; i < length; i++)
        {
            int px = x + dx;
            int py = y + i;
            if (px >= 0 && px < drawingTexture.width && py >= 0 && py < drawingTexture.height)
            {
                drawingTexture.SetPixel(px, py, drawColor);
            }
        }
    }

    void DrawArrow(int x, int y, bool up)
    {
        // 繪製箭頭
        int arrowLength = 10;
        for (int i = 0; i < arrowLength; i++)
        {
            int px = x;
            int py = y + (up ? i : -i);
            if (px >= 0 && px < drawingTexture.width && py >= 0 && py < drawingTexture.height)
            {
                drawingTexture.SetPixel(px, py, drawColor);
            }
        }
        // 繪製箭頭頭部
        int headSize = 3;
        for (int i = -headSize; i <= headSize; i++)
        {
            int px = x + (up ? i : -i);
            int py = y + (up ? arrowLength : -arrowLength);
            if (px >= 0 && px < drawingTexture.width && py >= 0 && py < drawingTexture.height)
            {
                drawingTexture.SetPixel(px, py, drawColor);
            }
        }
    }

    void DrawIrregularShape(int x, int y)
    {
        // 繪製不規則形狀
        for (int i = -5; i <= 5; i++)
        {
            for (int j = -5; j <= 5; j++)
            {
                if (i * i + j * j <= 25 && (i + j) % 2 == 0) // 簡單的不規則形狀
                {
                    int px = x + i;
                    int py = y + j;
                    if (px >= 0 && px < drawingTexture.width && py >= 0 && py < drawingTexture.height)
                    {
                        drawingTexture.SetPixel(px, py, drawColor);
                    }
                }
            }
        }
    }

    void DrawHeart(int x, int y)
    {
        // 繪製心形
        for (int i = -5; i <= 5; i++)
        {
            for (int j = -5; j <= 5; j++)
            {
                if ((i * i + j * j - 5 * Mathf.Abs(i) + 25) <= 0) // 簡單的心形公式
                {
                    int px = x + i;
                    int py = y + j;
                    if (px >= 0 && px < drawingTexture.width && py >= 0 && py < drawingTexture.height)
                    {
                        drawingTexture.SetPixel(px, py, drawColor);
                    }
                }
            }
        }
    }

    string GetShape()
    {
        // 這裡可以根據需要返回當前要繪製的圖案
        // 例如，可以隨機返回或根據其他條件返回
        return "heart"; // 範例返回
    }

    public void ClearDrawing()
    {
        // 清除绘图
        Color[] clearColor = new Color[drawingTexture.width * drawingTexture.height];
        for (int i = 0; i < clearColor.Length; i++)
        {
            clearColor[i] = Color.clear;
        }
        drawingTexture.SetPixels(clearColor);
        drawingTexture.Apply();
    }
}