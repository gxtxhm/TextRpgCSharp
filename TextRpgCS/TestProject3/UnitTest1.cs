using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextRpgCS;

namespace TextRpgCSTest3
{
    [TestClass]
    public class UnitTest1
    {
        // 성공
        //[TestMethod]
        //public void Item_ShouldStoreCorrectItemType()
        //{
        //    // Arrange
        //    var item = new HpPortion(); // 체력 물약 생성
        //    ItemManager.Instance.AddItem(item);

        //    // Act
        //    var retrievedItem = ItemManager.Instance.Inventory[item.Name][0];

        //    // Assert
        //    Assert.IsNotNull(retrievedItem);
        //    Assert.AreEqual(ItemType.HpPortion, retrievedItem.Type, "아이템 타입이 일치하지 않음!");
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
        //    Assert.AreEqual(GameManager.MonsterCount, monsterCount, "몬스터 개수가 올바르지 않음!");
        //}

        [TestMethod]
        public void Player_And_Monster_ShouldImplementIGameCharacter()
        {
            // Arrange
            var player = new Player();
            var monster = new Monster();

            // Assert
            Assert.IsTrue(player is IGameCharacter, "Player가 IGameCharacter를 구현하지 않음!");
            Assert.IsTrue(monster is IGameCharacter, "Monster가 IGameCharacter를 구현하지 않음!");
        }

    }
}