using Xunit;
using TextRpgCS; // ���� ������Ʈ�� ���ӽ����̽�

namespace TestProject1
{
    public class PlayerTests
    {
        [Fact]
        public void Attack_ShouldReduceMonsterHealth()
        {
            // Arrange
            var player = new Player { Name = "Hero", Hp = 100, AttackPower = 20 };
            var monster = new Monster { Name = "Goblin", Hp = 50 };

            // Act
            player.Attack(monster);

            // Assert
            Assert.Equal(30, monster.Hp); // ���� ü���� 50 - 20 = 30�̾�� ��.
        }

        [Fact]
        public void TakeDamage_ShouldReducePlayerHealth()
        {
            // Arrange
            var player = new Player { Name = "Hero", Hp = 100, AttackPower = 20 };

            // Act
            player.TakeDamage(30);

            // Assert
            Assert.Equal(70, player.Hp); // �÷��̾� ü���� 100 - 30 = 70�̾�� ��.
        }
    }

    public class MonsterTests
    {
        [Fact]
        public void TakeDamage_ShouldReduceMonsterHealth()
        {
            // Arrange
            var monster = new Monster { Name = "Goblin", Hp = 50 };

            // Act
            monster.TakeDamage(20);

            // Assert
            Assert.Equal(30, monster.Hp); // ���� ü���� 50 - 20 = 30�̾�� ��.
        }
    }
}
