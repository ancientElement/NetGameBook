using System;
using UnityEditor;
using NetGameRunning;

public class Test
{
    [MenuItem("Test/Test1")]
    public static void Test1()
    {
        PlayerMessage playerMessage = new PlayerMessage()
        {
            data = new PlayerData()
            {
                Position = new PositionData()
                {
                    X = 10026,
                    Y = 10035,
                }
            }
        };


        byte[] buffer = new byte[1024];

        playerMessage.GetBytes().CopyTo(buffer, 0);

        int id = BitConverter.ToInt32(buffer);
        int length = BitConverter.ToInt32(buffer, 4);

        UnityEngine.Debug.Log("id  " + id);
        UnityEngine.Debug.Log("length  " + length);

        PlayerMessage playerMessage1 = new PlayerMessage();


        playerMessage1.WriteIn(buffer, 8, length - 8);

        UnityEngine.Debug.Log("playerMessage1.data.Position.X  " + playerMessage1.data.Position.X);
        UnityEngine.Debug.Log("playerMessage1.data.Position.Y  " + playerMessage1.data.Position.Y);
    }
}
