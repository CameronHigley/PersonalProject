"use strict";
$(document).ready(() => {
    let counter = 1;
    $("#addIngredient").click(() => {
        $(".ingredients").append($('<br />'));
        $(".ingredients").append($('<label>Amount<input name="RecipeIngredients[' + counter.toString() + '].Amount" /></label>'));
        $(".ingredients").append($('<label>Ingredient<input name="Ingredients[' + counter.toString() + '].IngredientName" /></label>'));
        counter++;
    });
});