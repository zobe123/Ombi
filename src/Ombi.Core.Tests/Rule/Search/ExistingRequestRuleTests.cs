﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Ombi.Core.Models.Search;
using Ombi.Core.Rule.Rules.Search;
using Ombi.Store.Entities;
using Ombi.Store.Entities.Requests;
using Ombi.Store.Repository;
using Ombi.Store.Repository.Requests;

namespace Ombi.Core.Tests.Rule.Search
{
    public class ExistignRequestRuleTests
    {
        [SetUp]
        public void Setup()
        {

            MovieMock = new Mock<IMovieRequestRepository>();
            TvMock = new Mock<ITvRequestRepository>();
            Rule = new ExistingRule(MovieMock.Object, TvMock.Object);
        }

        private ExistingRule Rule { get; set; }
        private Mock<IMovieRequestRepository> MovieMock { get; set; }
        private Mock<ITvRequestRepository> TvMock { get; set; }


        [Test]
        public async Task ShouldBe_Requested_WhenExisitngMovie()
        {
            var list = new MovieRequests
            {
                TheMovieDbId = 123,
                Approved = true,
                RequestType = RequestType.Movie
            };

            MovieMock.Setup(x => x.GetRequest(123)).Returns(list);
            var search = new SearchMovieViewModel
            {
                Id = 123,
                

            };
            var result = await Rule.Execute(search);

            Assert.True(result.Success);
            Assert.True(search.Approved);
            Assert.True(search.Requested);
        }

        [Test]
        public async Task ShouldBe_NotRequested_WhenNewMovie()
        {
            var list = new MovieRequests
            {
                TheMovieDbId = 123,
                Approved = true
            };

            MovieMock.Setup(x => x.GetRequest(123)).Returns(list);
            var search = new SearchMovieViewModel
            {
                Id = 999,

            };
            var result = await Rule.Execute(search);

            Assert.True(result.Success);
            Assert.False(search.Approved);
            Assert.False(search.Requested);
        }

        [Test]
        public async Task ShouldBe_Requested_WhenExisitngTv()
        {
            var list = new TvRequests
            {
                TvDbId = 123,
                ChildRequests = new List<ChildRequests>
                {
                    new ChildRequests()
                    {
                        Approved = true

                    }
                }
            };

            TvMock.Setup(x => x.GetRequest(123)).Returns(list);
            var search = new SearchTvShowViewModel
            {
                Id = 123,
            };
            var result = await Rule.Execute(search);

            Assert.True(result.Success);
            Assert.True(search.Approved);
            Assert.True(search.Requested);
        }

        [Test]
        public async Task ShouldBe_NotRequested_WhenNewTv()
        {
            var list = new TvRequests
            {
                TvDbId = 123,
                ChildRequests = new List<ChildRequests>
                {
                    new ChildRequests()
                    {
                        Approved = true

                    }
                }
            };


            TvMock.Setup(x => x.GetRequest(123)).Returns(list);
            var search = new SearchTvShowViewModel()
            {
                Id = 999,

            };
            var result = await Rule.Execute(search);

            Assert.True(result.Success);
            Assert.False(search.Approved);
            Assert.False(search.Requested);
        }
    }
}