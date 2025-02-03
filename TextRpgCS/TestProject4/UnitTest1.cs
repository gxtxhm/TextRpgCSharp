using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TextRpgCS;

namespace TextRpgCSTests
{
    [TestClass]
    public class UnitTest_Mission4
    {
        [TestMethod]
        public void PlayerDeath_ShouldTriggerOnDeadEvent()
        {
            // Arrange
            var player = new Player();
            bool eventTriggered = false;

            player.OnDeadEvent += () => eventTriggered = true;

            // Act
            player.TakeDamage(999); // 체력보다 높은 데미지를 줘서 사망 유도

            // Assert
            Assert.IsTrue(eventTriggered, "플레이어 사망 이벤트가 트리거되지 않음!");
        }

        [TestMethod]
        public void MonsterDeath_ShouldTriggerOnDeadEvent()
        {
            // Arrange
            var monster = new Monster();
            bool eventTriggered = false;

            monster.OnDeadEvent += () => eventTriggered = true;

            // Act
            monster.TakeDamage(999); // 강제 사망

            // Assert
            Assert.IsTrue(eventTriggered, "몬스터 사망 이벤트가 트리거되지 않음!");
        }

        [TestMethod]
        public void PlayerAttack_ShouldTriggerOnAttackEvent()
        {
            // Arrange
            var player = new Player();
            var monster = new Monster();
            bool eventTriggered = false;

            player.OnAttackEvent += () => eventTriggered = true;

            // Act
            player.Attack(monster);

            // Assert
            Assert.IsTrue(eventTriggered, "플레이어 공격 이벤트가 트리거되지 않음!");
        }

        [TestMethod]
        public void MonsterAttack_ShouldTriggerOnAttackEvent()
        {
            // Arrange
            var monster = new Monster();
            var player = new Player();
            bool eventTriggered = false;

            monster.OnAttackEvent += () => eventTriggered = true;

            // Act
            monster.Attack(player);

            // Assert
            Assert.IsTrue(eventTriggered, "몬스터 공격 이벤트가 트리거되지 않음!");
        }

        [TestMethod]
        public void GameManager_ShouldHandlePlayerDeath()
        {
            // Arrange
            var gameManager = GameManager.Instance;
            gameManager.Init();
            bool eventTriggered = false;

            gameManager.Player.OnDeadEvent += () => eventTriggered = true;

            // Act
            gameManager.Player.TakeDamage(999); // 강제 사망

            // Assert
            Assert.IsTrue(eventTriggered, "GameManager에서 플레이어 사망 이벤트가 처리되지 않음!");
        }

        [TestMethod]
        public void GameManager_ShouldHandleMonsterDeath()
        {
            // Arrange
            var gameManager = GameManager.Instance;
            gameManager.Init();
            var monster = new Monster();
            bool eventTriggered = false;

            monster.OnDeadEvent += () => eventTriggered = true;

            // Act
            monster.TakeDamage(999); // 강제 사망

            // Assert
            Assert.IsTrue(eventTriggered, "GameManager에서 몬스터 사망 이벤트가 처리되지 않음!");
        }
    }
}
