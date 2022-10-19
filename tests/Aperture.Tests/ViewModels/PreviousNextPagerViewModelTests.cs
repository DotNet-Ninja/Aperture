using Aperture.Constants;
using Aperture.Models;
using FluentAssertions;

namespace Aperture.Tests.ViewModels;

public class PreviousNextPagerViewModelTests
{
    [Fact]
    public void WhenCurrentPageIsFirstPage_CanGoBack_ShouldBeFalse()
    {
        var entities = TestEntity.CreateTestEntities(3);
        var page = new Page<TestEntity>(3, 1, 12, entities); // Page 1 of 1 - 12 items per page - 3 entities total 
        var model = new PreviousNextPagerViewModel(page);

        model.CanGoBack.Should().BeFalse();
    }

    [Fact]
    public void WhenCurrentPageIsNotFirstPage_CanGoBack_ShouldBeTrue()
    {
        var entities = TestEntity.CreateTestEntities(3);
        var page = new Page<TestEntity>(15, 2, 12, entities); // Page 2 of 2 - 12 items per page - 15 entities total 
        var model = new PreviousNextPagerViewModel(page);

        model.CanGoBack.Should().BeTrue();
    }

    [Fact]
    public void WhenCurrentPageIsNotLastPage_CanGoForward_ShouldBeTrue()
    {
        var entities = TestEntity.CreateTestEntities(12);
        var page = new Page<TestEntity>(15, 1, 12, entities); // Page 1 of 2 - 12 items per page - 15 entities total 
        var model = new PreviousNextPagerViewModel(page);

        model.CanGoForward.Should().BeTrue();
    }

    [Fact]
    public void WhenCurrentPageIsLastPage_CanGoForward_ShouldBeFalse()
    {
        var entities = TestEntity.CreateTestEntities(3);
        var page = new Page<TestEntity>(15, 2, 12, entities); // Page 2 of 2 - 12 items per page - 15 entities total 
        var model = new PreviousNextPagerViewModel(page);

        model.CanGoForward.Should().BeFalse();
    }

    [Fact]
    public void PreviousText_ShouldDefaultToBack()
    {
        var entities = TestEntity.CreateTestEntities(3);
        var page = new Page<TestEntity>(3, 1, 12, entities); // Page 1 of 1 - 12 items per page - 3 entities total 
        var model = new PreviousNextPagerViewModel(page);

        model.PreviousText.Should().Be("Back");
    }

    [Fact]
    public void NextText_ShouldDefaultToNext()
    {
        var entities = TestEntity.CreateTestEntities(3);
        var page = new Page<TestEntity>(3, 1, 12, entities); // Page 1 of 1 - 12 items per page - 3 entities total 
        var model = new PreviousNextPagerViewModel(page);

        model.NextText.Should().Be("Next");
    }

    [Fact]
    public void NextText_ShouldBeOverridable()
    {
        const string text = "Overridden";
        var entities = TestEntity.CreateTestEntities(3);
        var page = new Page<TestEntity>(3, 1, 12, entities); // Page 1 of 1 - 12 items per page - 3 entities total 
        var model = new PreviousNextPagerViewModel(page)
        {
            NextText = text
        };

        model.NextText.Should().Be(text);
    }

    [Fact]
    public void PreviousText_ShouldBeOverridable()
    {
        const string text = "Overridden";
        var entities = TestEntity.CreateTestEntities(3);
        var page = new Page<TestEntity>(3, 1, 12, entities); // Page 1 of 1 - 12 items per page - 3 entities total 
        var model = new PreviousNextPagerViewModel(page)
        {
            PreviousText = text
        };

        model.PreviousText.Should().Be(text);
    }

    [Fact]
    public void PreviousUrl_ShouldDefaultToApplicationRoot()
    {
        var entities = TestEntity.CreateTestEntities(3);
        var page = new Page<TestEntity>(3, 1, 12, entities); // Page 1 of 1 - 12 items per page - 3 entities total 
        var model = new PreviousNextPagerViewModel(page);

        model.PreviousUrl.Should().Be(WellKnownEndpoint.ApplicationRoot);
    }

    [Fact]
    public void NextUrl_ShouldDefaultToApplicationRoot()
    {
        var entities = TestEntity.CreateTestEntities(3);
        var page = new Page<TestEntity>(3, 1, 12, entities); // Page 1 of 1 - 12 items per page - 3 entities total 
        var model = new PreviousNextPagerViewModel(page);

        model.NextUrl.Should().Be(WellKnownEndpoint.ApplicationRoot);
    }
    
    [Fact]
    public void PreviousUrl_ShouldBeOverridable()
    {
        const string url = "https://www.example.com/target";
        var entities = TestEntity.CreateTestEntities(3);
        var page = new Page<TestEntity>(3, 1, 12, entities); // Page 1 of 1 - 12 items per page - 3 entities total 
        var model = new PreviousNextPagerViewModel(page)
        {
            PreviousUrl = url
        };

        model.PreviousUrl.Should().Be(url);
    }

    [Fact]
    public void NextUrl_ShouldBeOverridable()
    {
        const string url = "https://www.example.com/target";
        var entities = TestEntity.CreateTestEntities(3);
        var page = new Page<TestEntity>(3, 1, 12, entities); // Page 1 of 1 - 12 items per page - 3 entities total 
        var model = new PreviousNextPagerViewModel(page)
        {
            NextUrl = url
        };

        model.NextUrl.Should().Be(url);
    }

    [Fact]
    public void WhenPageNumberIsOne_CanGoBack_ShouldBeFalse()
    {
        var entities = TestEntity.CreateTestEntities(3);
        var page = new Page<TestEntity>(3, 1, 12, entities); // Page 1 of 1 - 12 items per page - 3 entities total 
        var model = new PreviousNextPagerViewModel(page);

        model.CanGoBack.Should().BeFalse();
    }

    [Fact]
    public void WhenPageNumberIsGreaterThanOne_CanGoBack_ShouldBeTrue()
    {
        var entities = TestEntity.CreateTestEntities(3);
        var page = new Page<TestEntity>(15, 2, 12, entities); // Page 1 of 1 - 12 items per page - 3 entities total 
        var model = new PreviousNextPagerViewModel(page);

        model.CanGoBack.Should().BeTrue();
    }


    [Fact]
    public void WhenPageNumberIsLastPage_CanGoForward_ShouldBeFalse()
    {
        var entities = TestEntity.CreateTestEntities(3);
        var page = new Page<TestEntity>(3, 2, 15, entities); // Page 2 of 2 - 12 items per page - 15 entities total 
        var model = new PreviousNextPagerViewModel(page);

        model.CanGoForward.Should().BeFalse();
    }

    [Fact]
    public void WhenPageNumberIsLessThanLastPage_CanGoForward_ShouldBeTrue()
    {
        var entities = TestEntity.CreateTestEntities(3);
        var page = new Page<TestEntity>(15, 1, 12, entities); // Page 1 of 2 - 12 items per page - 15 entities total 
        var model = new PreviousNextPagerViewModel(page);

        model.CanGoForward.Should().BeTrue();
    }
}