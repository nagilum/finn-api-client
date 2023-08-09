namespace FinnApi;

public enum ResourceType
{
    [FinnPartialUrl("agriculture-thresher")]
    AgricultureThresher,

    [FinnPartialUrl("agriculture-tools")]
    AgricultureTools,

    [FinnPartialUrl("agriculture-tractor")]
    AgricultureTractor,

    [FinnPartialUrl("bap")]
    Bap,

    [FinnPartialUrl("bap-webstore")]
    BapWebstore,

    [FinnPartialUrl("boat-dock")]
    BoatDock,

    [FinnPartialUrl("boat-motor")]
    BoatMotor,

    [FinnPartialUrl("boat-new-sale")]
    BoatNewSale,

    [FinnPartialUrl("boat-rent")]
    BoatRent,

    [FinnPartialUrl("boat-used-sale")]
    BoatUsedSale,

    [FinnPartialUrl("boat-used-wanted")]
    BoatUsedWanted,

    [FinnPartialUrl("bus")]
    Bus,

    [FinnPartialUrl("caravan")]
    Caravan,

    [FinnPartialUrl("car-leasing")]
    CarLeasing,

    [FinnPartialUrl("car-new-sale")]
    CarNewSale,

    [FinnPartialUrl("car-used-sale")]
    CarUsedSale,

    [FinnPartialUrl("company-for-sale")]
    CompanyForSale,

    [FinnPartialUrl("construction")]
    Construction,

    [FinnPartialUrl("job-full-time")]
    JobFullTime,

    [FinnPartialUrl("job-management")]
    JobManagement,

    [FinnPartialUrl("job-part-time")]
    JobPartTime,

    [FinnPartialUrl("mc")]
    MC,

    [FinnPartialUrl("mobile-home")]
    MobileHome,

    [FinnPartialUrl("realestate-abroad-sale")]
    RealestateAbroadSale,

    [FinnPartialUrl("realestate-business-letting")]
    RealestateBusinessLetting,

    [FinnPartialUrl("realestate-business-plot")]
    RealestateBusinessPlot,

    [FinnPartialUrl("realestate-business-sale")]
    RealestateBusinessSale,

    [FinnPartialUrl("realestate-development")]
    RealestateDevelopment,

    [FinnPartialUrl("realestate-development-single")]
    RealestateDevelopmentSingle,

    [FinnPartialUrl("realestate-homes")]
    RealestateHomes,

    [FinnPartialUrl("realestate-leisure-sale")]
    RealestateLeisureSale,

    [FinnPartialUrl("realestate-letting")]
    RealestateLetting,

    [FinnPartialUrl("realestate-letting-wanted")]
    RealestateLettingWanted,

    [FinnPartialUrl("realestate-planned")]
    RealestatePlanned,

    [FinnPartialUrl("realestate-plot")]
    RealestatePlot,

    [FinnPartialUrl("realestate-project")]
    RealestateProject,

    [FinnPartialUrl("realestate-project-unit")]
    RealestateProjectUnit,

    [FinnPartialUrl("travel-fhh")]
    TravelFhh,

    [FinnPartialUrl("truck")]
    Truck
}