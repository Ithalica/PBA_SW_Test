describe("My first test", function () {
    it("Reset", function () {
        cy.visit("/");
    });

    it("Main page contains needed elements",
        function () {
            cy.contains("Create New");
            cy.contains("Next year");
            cy.contains("Previous year");
        });
});

describe("Create new booking as John", function () {
    it("Reset", function () {
        cy.visit("/");
    });
    it("Perform booking", function () {

        cy.contains("Create New").click();

        cy.get("#StartDate").type("2018-01-01");
        cy.get("#EndDate").type("2018-01-31");

        cy.get("form").submit();
    });

    it("Is on start page", function () {
        cy.contains("Create New");
    });
});

describe("Create new booking as Jane", function () {
    it("Reset", function () {
        cy.visit("/");
    });
    it("Perform booking", function () {

        cy.contains("Create New").click();

        cy.get("#StartDate").type("2018-01-01");
        cy.get("#EndDate").type("2018-01-31");

        cy.get("select").select("2");
        cy.get("form").submit();
    });

    it("Is on start page", function () {
        cy.contains("Create New");
    });
});

describe("Create new booking as Jane", function () {
    it("Reset", function () {
        cy.visit("/");
    });
    it("Perform booking", function () {

        cy.contains("Create New").click();

        cy.get("#StartDate").type("2018-01-01");
        cy.get("#EndDate").type("2018-01-31");

        cy.get("select").select("2");
        cy.get("form").submit();
    });

    it("Is on start page", function () {
        cy.contains("Create New");
    });
});

describe("Create new booking fails", function () {
    it("Reset", function () {
        cy.visit("/");
    });
    it("Perform booking", function () {

        cy.contains("Create New").click();

        cy.get("#StartDate").type("2018-01-01");
        cy.get("#EndDate").type("2018-01-31");

        cy.get("form").submit();
    });

    it("Booking failed", function () {
        cy.get("h4.text-danger").contains("The booking could not be created. There were no available room.");
    });
});

describe("Create new booking fail due to start date being in the past", function () {
    it("Reset", function () {
        cy.visit("/");
    });

    it("Perform booking", function () {
        cy.contains("Create New").click();

        cy.get("#StartDate").type("2016-01-01");
        cy.get("#EndDate").type("2016-01-31");

        cy.get("select").select("2");
        cy.get("form").submit();


    });

    it("Booking failed", function () {
        cy.get("h4.text-danger").contains("The start date cannot be in the past or later than the end date.");
    });
});

describe("Create new booking fail due to end date being before start date", function () {
    it("Reset", function () {
        cy.visit("/");
    });

    it("Perform booking", function () {
        cy.contains("Create New").click();

        cy.get("#StartDate").type("2019-01-31");
        cy.get("#EndDate").type("2019-01-01");

        cy.get("select").select("2");
        cy.get("form").submit();


    });

    it("Booking failed", function () {
        cy.get("h4.text-danger").contains("The start date cannot be in the past or later than the end date.");
    });
});

describe("Create new booking fail due to invalid start data", function () {
    it("Reset", function () {
        cy.visit("/");
    });

    it("Perform booking", function () {
        cy.contains("Create New").click();

        cy.get("#EndDate").type("2019-01-01");

        cy.get("select").select("2");
        cy.get("form").submit();


    });

    it("Booking failed", function () {
        cy.get("#StartDate-error").contains("The StartDate field is required.");
    });
});


describe("Create new booking fail due to invalid end data", function () {
    it("Reset", function () {
        cy.visit("/");
    });

    it("Perform booking", function () {
        cy.contains("Create New").click();

        cy.get("#StartDate").type("2019-01-01");

        cy.get("select").select("2");
        cy.get("form").submit();


    });

    it("Booking failed", function () {
        cy.get("#EndDate-error").contains("The EndDate field is required.");
    });
});