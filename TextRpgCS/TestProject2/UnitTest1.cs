using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextRpgCS;

namespace TextRpgCSTests
{
    [TestClass]
    public class ItemManagerTests
    {
        private ItemManager itemManager=ItemManager.Instance;
        private GameManager gm = GameManager.Instance;
        
        [TestInitialize]
        public void Setup()
        {
            gm.Init();
            itemManager = ItemManager.Instance;
            itemManager.Inventory.Clear();
            itemManager.DurationItems.Clear();
        }

        [TestMethod]
        public void AddItem_ShouldIncreaseInventoryCount()
        {
            // Arrange
            var item = new HpPotion();

            // Act
            itemManager.AddItem(item);
            int itemCount = itemManager.Inventory[item.Name].Count;

            // Assert
            Assert.AreEqual(1, itemCount);
        }

        [TestMethod]
        public void UseItem_ShouldRemoveFromInventory()
        {
            // Arrange
            var item = new HpPotion();
            itemManager.AddItem(item);

            // Act
            itemManager.RemoveItem(item.Name);

            // Assert
            Assert.IsFalse(itemManager.Inventory.ContainsKey(item.Name));
        }

        [TestMethod]
        public void RegistItem_ShouldRegisterBuffItem()
        {
            // Arrange
            var item = new AttackPotion();

            // Act
            itemManager.RegistItem(item);

            // Assert
            Assert.IsTrue(itemManager.DurationItems.ContainsKey(item.Name));
            Assert.AreEqual(3, itemManager.DurationItems[item.Name].Duration);
        }

        [TestMethod]
        public void Update_ShouldDecreaseBuffDuration()
        {
            // Arrange
            var item = new AttackPotion();
            itemManager.RegistItem(item);

            // Act
            itemManager.Update();

            // Assert
            Assert.AreEqual(2, itemManager.DurationItems[item.Name].Duration);
        }

        [TestMethod]
        public void Update_ShouldRemoveExpiredBuffItem()
        {
            // Arrange
            var item = new AttackPotion();
            itemManager.RegistItem(item);

            // 🟢 Null 방지를 위한 로그 확인
            Assert.IsTrue(itemManager.DurationItems.ContainsKey(item.Name), "[ERROR] 버프 등록 실패!");

            // Act
            itemManager.Update();
            itemManager.Update();
            itemManager.Update(); // 3턴 지나면 삭제되어야 함.

            // Assert
            Assert.IsFalse(itemManager.DurationItems.ContainsKey(item.Name), "[ERROR] 버프가 정상적으로 삭제되지 않음.");
        }
    }
}
