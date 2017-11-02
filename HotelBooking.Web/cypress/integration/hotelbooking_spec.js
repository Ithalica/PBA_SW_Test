describe("My first test", function () {
    it("Main page contains needed elements",
        function () {
            cy.visit("/");

            cy.contains("Create New");

            cy.contains("Next year");

            cy.contains("Previous year");
        });
});

describe("Create new booking", function () {
    it("Go to booking page", function () {
        cy.contains("Create New").click();
    });

    it("Create booking", function () {
        cy.get("#StartDate").type("2018-01-01");
        cy.get("#EndDate").type("2018-01-31");
        cy.get("select").select("2");
        cy.get("form").submit();
    });
});
