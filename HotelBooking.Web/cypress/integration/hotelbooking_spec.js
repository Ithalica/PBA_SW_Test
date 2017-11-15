describe("Hotel Booking Test", function () {
    //The real way to do this would be to have an API you could query to create the needed data
    //This approach will create snapshots and slow down the test
    //before(function () {
    //    for (var i = 0; i < 3; i++) {
    //        cy.TestSetup();
    //        cy.contains("Create New").click();
    //        cy.BookingStartDate(16);
    //        cy.BookingEndDate(19);
    //        cy.get("form").submit();
    //    }
    //});

    beforeEach(function () {
        cy.TestSetup();
    });

    describe("Main page contains needed elements", function () {
        it("Main page contains needed elements",
            function () {
                cy.contains("Create New");
                cy.contains("Next year");
                cy.contains("Previous year");
            });
    });

    describe("Main page contains full booked dates", function () {
        it("Check for full booked days", function () {
            var startDate = new Date();
            startDate.setDate(startDate.getDate() + 20);

            var endDate = new Date();
            endDate.setDate(endDate.getDate() + 23);

            var dateArray = new Array();
            var currentDate = startDate;
            while (currentDate <= endDate) {
                dateArray.push(new Date(currentDate));
                currentDate.setDate(currentDate.getDate() + 1);
            }

            for (var i = 0; i < dateArray.length; i++) {
                var date = dateArray[i];
                cy.get(".day-" + date.getDate() + ".month-" + (date.getMonth() + 1));
            }
        });
    });

    describe("Create new booking as John", function () {

        it("Perform booking", function () {

            cy.contains("Create New").click();

            cy.BookingStartDate(30);
            cy.BookingEndDate(40);

            cy.get("form").submit();

            cy.contains("Create New");
        });
    });

    describe("Create new booking as Jane", function () {

        it("Perform booking", function () {

            cy.contains("Create New").click();

            cy.BookingStartDate(30);
            cy.BookingEndDate(40);

            cy.get("select").select("2");
            cy.get("form").submit();

            cy.contains("Create New");
        });
    });

    describe("Create new booking fails", function () {

        it("Perform failing booking", function () {

            cy.contains("Create New").click();

            //Because of seeded bookings in the in memory DB the booking on indicated dates will fail
            //If the DB context is changed to a context without the same seeded data, the test will fail
            cy.BookingStartDate(16);
            cy.BookingEndDate(19);

            cy.get("form").submit();

            cy.get("h4.text-danger").contains("The booking could not be created. There were no available room.");
        });

    });

    describe("Create new booking fail due to start date being in the past", function () {

        it("Perform failing booking", function () {
            cy.contains("Create New").click();

            cy.get("#StartDate").type("2016-01-01");
            cy.get("#EndDate").type("2016-01-31");

            cy.get("select").select("2");
            cy.get("form").submit();

            cy.get("h4.text-danger").contains("The start date cannot be in the past or later than the end date.");
        });
    });

    describe("Create new booking fail due to end date being before start date", function () {
        it("Perform failing booking", function () {
            cy.contains("Create New").click();

            cy.get("#StartDate").type("2019-01-31");
            cy.get("#EndDate").type("2019-01-01");

            cy.get("select").select("2");
            cy.get("form").submit();

            cy.get("h4.text-danger").contains("The start date cannot be in the past or later than the end date.");
        });
    });

    describe("Create new booking fail due to invalid start data", function () {

        it("Perform failing booking", function () {
            cy.contains("Create New").click();

            cy.get("#EndDate").type("2019-01-01");

            cy.get("select").select("2");
            cy.get("form").submit();

            cy.get("#StartDate-error").contains("The StartDate field is required.");
        });
    });

    describe("Create new booking fail due to invalid end data", function () {
        it("Perform failing booking", function () {
            cy.contains("Create New").click();

            cy.get("#StartDate").type("2019-01-01");

            cy.get("select").select("2");
            cy.get("form").submit();

            cy.get("#EndDate-error").contains("The EndDate field is required.");
        });
    });


});

