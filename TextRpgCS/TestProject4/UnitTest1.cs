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
            player.TakeDamage(999); // ü�º��� ���� �������� �༭ ��� ����

            // Assert
            Assert.IsTrue(eventTriggered, "�÷��̾� ��� �̺�Ʈ�� Ʈ���ŵ��� ����!");
        }

        [TestMethod]
        public void MonsterDeath_ShouldTriggerOnDeadEvent()
        {
            // Arrange
            var monster = new Monster();
            bool eventTriggered = false;

            monster.OnDeadEvent += () => eventTriggered = true;

            // Act
            monster.TakeDamage(999); // ���� ���

            // Assert
            Assert.IsTrue(eventTriggered, "���� ��� �̺�Ʈ�� Ʈ���ŵ��� ����!");
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
            Assert.IsTrue(eventTriggered, "�÷��̾� ���� �̺�Ʈ�� Ʈ���ŵ��� ����!");
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
            Assert.IsTrue(eventTriggered, "���� ���� �̺�Ʈ�� Ʈ���ŵ��� ����!");
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
            gameManager.Player.TakeDamage(999); // ���� ���

            // Assert
            Assert.IsTrue(eventTriggered, "GameManager���� �÷��̾� ��� �̺�Ʈ�� ó������ ����!");
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
            monster.TakeDamage(999); // ���� ���

            // Assert
            Assert.IsTrue(eventTriggered, "GameManager���� ���� ��� �̺�Ʈ�� ó������ ����!");
        }
    }
}
