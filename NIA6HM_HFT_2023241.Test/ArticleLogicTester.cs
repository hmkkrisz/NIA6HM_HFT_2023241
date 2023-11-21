using System;
using System.Collections.Generic;
using System.Linq;
using NIA6HM_HFT_2023241.Logic;
using NIA6HM_HFT_2023241.Repository;
using NIA6HM_HFT_2023241.Models;
using Moq;
using NUnit.Framework;
using static NIA6HM_HFT_2023241.Logic.ArticleLogic;


namespace NIA6HM_HFT_2023241.Test
{
    [TestFixture]
    public class ArticleLogicTester
    {
        ArticleLogic articleLogic;
        Mock<IRepository<Article>> mockArticleRepo;

        [SetUp]
        public void Init()
        {
            mockArticleRepo = new Mock<IRepository<Article>>();
            mockArticleRepo.Setup(m => m.ReadAll()).Returns(new List<Article>()
            {
                new Article{ArticleId=1,Title="TitleA",Category="Sci-Fi", Likes=10},
                new Article{ArticleId=2,Title="TitleB",Category="Sci-Fi", Likes=20},
                new Article{ArticleId=3,Title="TitleC",Category="Sci-Fi", Likes=30},
                new Article{ArticleId=4,Title="TitleD",Category="Sci-Fi", Likes=40}
            }.AsQueryable());


        }

    }
}
