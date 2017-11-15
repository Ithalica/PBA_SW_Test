// ***********************************************
// This example commands.js shows you how to
// create various custom commands and overwrite
// existing commands.
//
// For more comprehensive examples of custom
// commands please read more here:
// https://on.cypress.io/custom-commands
// ***********************************************
//
//
// -- This is a parent command --
// Cypress.Commands.add("login", (email, password) => { ... })
//
//
// -- This is a child command --
// Cypress.Commands.add("drag", { prevSubject: 'element'}, (subject, options) => { ... })
//
//
// -- This is a dual command --
// Cypress.Commands.add("dismiss", { prevSubject: 'optional'}, (subject, options) => { ... })
//
//
// -- This is will overwrite an existing command --
// Cypress.Commands.overwrite("visit", (originalFn, url, options) => { ... })

Cypress.Commands.add("TestSetup", () => {
    cy.visit("/");
});

Cypress.Commands.add("BookingStartDate", (dayOffset) => {

    var date = new Date();
    if (!dayOffset) {
        dayOffset = 1;
    }

    date.setDate(date.getDate() + dayOffset);
    var day = date.getDate() > 9 ? date.getDate() : "0" + date.getDate();

    var dateString = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + day;

    cy.get("#StartDate").type(dateString);
});

Cypress.Commands.add("BookingEndDate", (dayOffset) => {
    var date = new Date();

    if (!dayOffset) {
        dayOffset = 14;
    }

    date.setDate(date.getDate() + dayOffset);
    var day = date.getDate() > 9 ? date.getDate() : "0" + date.getDate();
    var dateString = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + day;

    cy.get("#EndDate").type(dateString);
});
