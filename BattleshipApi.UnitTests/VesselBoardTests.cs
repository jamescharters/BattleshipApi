using BattleshipApi.BusinessLogic.Factories;
using BattleshipApi.BusinessLogic.Models;

namespace BattleshipApi.UnitTests;

public class VesselBoardTests
{
    // private Mock<IVesselFactory> mockVesselFactory;

    [SetUp]
    public void Setup()
    {
        // mockVesselFactory = new Mock<IVesselFactory>();
        //
        // mockVesselFactory.Setup(_ => _.Create(It.IsAny<int>())).Returns(new Vessel()
        // {
        //     Damage = 0,
        //     Id = Guid.Empty,
        //     Name = "FakeVessel",
        //     Size = 4
        // });
    }
    
    [Test]
    public Task it_should_return_graphic_in_tostring()
    {
        var sut = new VesselBoard(new VesselFactory(), 10);
        
        sut.AddVessel(new Coordinate(0, 0), VesselOrientation.Horizontal, 5);

        return Verifier.Verify(sut.ToString());
    }
    
    [Test]
    public Task it_should_return_graphic_in_tostring_vertical()
    {
        var sut = new VesselBoard(new VesselFactory(), 10);
        
        sut.AddVessel(new Coordinate(1, 1), VesselOrientation.Vertical, 5);

        return Verifier.Verify(sut.ToString());
    }
    
    [Test]
    public Task it_should_return_graphic_in_tostring_multiple()
    {
        var sut = new VesselBoard(new VesselFactory(), 10);
        
        sut.AddVessel(new Coordinate(0, 0), VesselOrientation.Horizontal, 5);
        sut.AddVessel(new Coordinate(1, 3), VesselOrientation.Horizontal, 4);
        sut.AddVessel(new Coordinate(5, 5), VesselOrientation.Vertical, 5);

        return Verifier.Verify(sut.ToString());
    }

    [Test]
    public void it_should_throw_exception_if_vessels_intersect()
    {
        var sut = new VesselBoard(new VesselFactory(), 3);
        
        sut.AddVessel(new Coordinate(0, 0), VesselOrientation.Horizontal, 1);
        // sut.AddVessel(new Coordinate(1, 0), VesselOrientation.Horizontal, 2);

        var x = sut.ToString();

    }
}