using Xunit;
using TextRpgCS; // 게임 프로젝트의 네임스페이스

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
            Assert.Equal(30, monster.Hp); // 몬스터 체력이 50 - 20 = 30이어야 함.
        }

        [Fact]
        public void TakeDamage_ShouldReducePlayerHealth()
        {
            // Arrange
            var player = new Player { Name = "Hero", Hp = 100, AttackPower = 20 };

            // Act
            player.TakeDamage(30);

            // Assert
            Assert.Equal(70, player.Hp); // 플레이어 체력이 100 - 30 = 70이어야 함.
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
            Assert.Equal(30, monster.Hp); // 몬스터 체력이 50 - 20 = 30이어야 함.
        }
    }
}
