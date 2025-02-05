using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Collections.Generic;
using TextRpgCS;

[TestClass]
public class ExceptionHandlingTests
{
    private GameManager gameManager;
    private Player player;

    [TestInitialize]
    public void Setup()
    {
        gameManager = new GameManager();
        player = new Player();
        gameManager.Init();
    }


    [TestMethod]
    public void UseItem_ItemNotFound_ShouldLogError()
    {
        // Arrange
        string nonExistentItem = "MagicPotion";

        // Act
        ItemManager.Instance.UsedItem(nonExistentItem, player);

        // 로그 파일 검증 전에 짧은 대기 시간 추가 (로그 파일이 기록될 시간을 주기 위함)
        System.Threading.Thread.Sleep(100);

        // Assert
        string log = File.ReadAllText("error_log3.txt");
        Assert.IsTrue(log.Contains("예외 발생"));
    }


    [TestMethod]
    public void LogError_ShouldWriteToFile()
    {
        // Arrange
        Exception testException = new Exception("테스트 예외");

        // Act
        ItemManager.Instance.LogError(testException);

        // Assert
        string log = File.ReadAllText("error_log3.txt");
        Assert.IsTrue(log.Contains("테스트 예외"));
    }
}
