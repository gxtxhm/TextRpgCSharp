using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextRpgCS;

namespace TextRpgCSTest3
{
    [TestClass]
    public class UnitTest1
    {
        // ����
        //[TestMethod]
        //public void Item_ShouldStoreCorrectItemType()
        //{
        //    // Arrange
        //    var item = new HpPortion(); // ü�� ���� ����
        //    ItemManager.Instance.AddItem(item);

        //    // Act
        //    var retrievedItem = ItemManager.Instance.Inventory[item.Name][0];

        //    // Assert
        //    Assert.IsNotNull(retrievedItem);
        //    Assert.AreEqual(ItemType.HpPortion, retrievedItem.Type, "������ Ÿ���� ��ġ���� ����!");
        //}


        //[TestMethod]
        //public void GameManager_ShouldInitializeMonsterList()
        //{
        //    // Arrange
        //    var gm = GameManager.Instance;
        //    gm.Init();

        //    // Act
        //    int monsterCount = gm.monsters.Count;

        //    // Assert
        //    Assert.AreEqual(GameManager.MonsterCount, monsterCount, "���� ������ �ùٸ��� ����!");
        //}

        [TestMethod]
        public void Player_And_Monster_ShouldImplementIGameCharacter()
        {
            // Arrange
            var player = new Player();
            var monster = new Monster();

            // Assert
            Assert.IsTrue(player is IGameCharacter, "Player�� IGameCharacter�� �������� ����!");
            Assert.IsTrue(monster is IGameCharacter, "Monster�� IGameCharacter�� �������� ����!");
        }

    }
}