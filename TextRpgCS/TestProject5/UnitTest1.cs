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

        // �α� ���� ���� ���� ª�� ��� �ð� �߰� (�α� ������ ��ϵ� �ð��� �ֱ� ����)
        System.Threading.Thread.Sleep(100);

        // Assert
        string log = File.ReadAllText("error_log3.txt");
        Assert.IsTrue(log.Contains("���� �߻�"));
    }


    [TestMethod]
    public void LogError_ShouldWriteToFile()
    {
        // Arrange
        Exception testException = new Exception("�׽�Ʈ ����");

        // Act
        ItemManager.Instance.LogError(testException);

        // Assert
        string log = File.ReadAllText("error_log3.txt");
        Assert.IsTrue(log.Contains("�׽�Ʈ ����"));
    }
}
