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
        var sut = new VesselBoard(10);

        var vessel = new Vessel("Fake", 5);
        
        sut.AddVessel(new Coordinate(0, 0), VesselOrientation.Horizontal, vessel);

        return Verifier.Verify(sut.ToString());
    }
    
    [Test]
    public Task it_should_return_graphic_in_tostring_vertical()
    {
        var sut = new VesselBoard(10);
        
        var vessel = new Vessel("Fake", 5);
        
        sut.AddVessel(new Coordinate(1, 1), VesselOrientation.Vertical, vessel);

        return Verifier.Verify(sut.ToString());
    }
    
    [Test]
    public Task it_should_return_graphic_in_tostring_multiple()
    {
        var sut = new VesselBoard(10);
        
        var vessel = new Vessel("Fake", 5);
        
        sut.AddVessel(new Coordinate(0, 0), VesselOrientation.Horizontal, vessel);
        sut.AddVessel(new Coordinate(1, 3), VesselOrientation.Horizontal, vessel);
        sut.AddVessel(new Coordinate(5, 5), VesselOrientation.Vertical, vessel);

        return Verifier.Verify(sut.ToString());
    }

    [Test]
    public void it_should_throw_exception_if_vessels_intersect()
    {
        var sut = new VesselBoard(3);
        
        var vessel = new Vessel("Fake", 5);
        
        sut.AddVessel(new Coordinate(0, 0), VesselOrientation.Horizontal, vessel);
        // sut.AddVessel(new Coordinate(1, 0), VesselOrientation.Horizontal, 2);

        var x = sut.ToString();

    }
}