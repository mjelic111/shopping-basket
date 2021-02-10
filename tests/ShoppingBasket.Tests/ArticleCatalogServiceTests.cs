using BasketLibrary.Models;
using FluentAssertions;
using Xunit;

namespace ShoppingBasket.Tests
{
    public class ArticleCatalogServiceTests : BaseTest
    {
        [Fact]
        public void ClearArticle_ShouldBeEmpty()
        {
            // arrange
            var count = 0;
            // act
            var response = articleCatalogService.ClearCatalog();
            // assert
            response.Data.Should().Be($"Celared catalog, current items in catalog: {count}");
        }

        [Fact]
        public void ClearArticle_CountCannotBeGreaterThanZero()
        {
            // arrange
            // act
            var response = articleCatalogService.ClearCatalog();
            var count = int.MaxValue;
            int.TryParse(response.Data.Substring(response.Data.IndexOf(':')).Trim(), out count);
            // assert
            count.Should().BeLessOrEqualTo(0);
        }

        [Fact]
        public void FindArticeById_ShouldFind()
        {
            // arrange
            var id = InitArticlesCatalog();
            // act
            var response = articleCatalogService.FindArticeById(id);
            // assert
            response.IsError.Should().Be(false);
            response.Data.Id.Should().Be(id);
        }

        [Fact]
        public void FindArticeById_ShouldNotFind()
        {
            // arrange
            var id = GenerateGuid();
            InitArticlesCatalog();
            // act
            var response = articleCatalogService.FindArticeById(id);
            // assert
            response.IsError.Should().Be(true);
            response.ErrorMessage.Should().Be($"Article with id: {id} not found!");
        }

        [Fact]
        public void FindArticeByName_ShouldFind()
        {
            // arrange
            var name = "milk";
            InitArticlesCatalog();
            // act
            var response = articleCatalogService.FindArticeByName(name);
            // assert
            response.IsError.Should().Be(false);
            response.Data.Name.ToLower().Should().Be(name);
        }

        [Fact]
        public void FindArticeByName_ShouldNotFind()
        {
            // arrange
            var name = "test";
            InitArticlesCatalog();
            // act
            var response = articleCatalogService.FindArticeByName(name);
            // assert
            response.IsError.Should().Be(true);
            response.ErrorMessage.Should().Be($"Article with name: {name} not found!");
        }

        [Fact]
        public void RegisterArticle_ShouldSuccessfullyRegister()
        {
            // arrange
            var article = new ArticleDto { Id = GenerateGuid(), Name = "Test", Price = 100.50 };
            // act
            var response = articleCatalogService.RegisterArticle(article);
            // assert
            response.Data.Should().Be("Article registered!");
        }

        [Fact]
        public void RegisterArticle_DuplicateShouldNotRegister_UniqueName()
        {
            // arrange
            InitArticlesCatalog();
            var article = new ArticleDto { Id = GenerateGuid(), Name = "Milk", Price = 100.50 };
            // act
            var response = articleCatalogService.RegisterArticle(article);
            // assert
            response.IsError.Should().Be(true);
            response.ErrorMessage.Should().Be("Article allready registered!");
        }

        [Fact]
        public void RegisterArticle_DuplicateShouldNotRegister_UniqueId()
        {
            // arrange
            var id = InitArticlesCatalog();
            var article = new ArticleDto { Id = id, Name = "Test", Price = 100.50 };
            // act
            var response = articleCatalogService.RegisterArticle(article);
            // assert
            response.IsError.Should().Be(true);
            response.ErrorMessage.Should().Be("Article allready registered!");
        }
    }
}