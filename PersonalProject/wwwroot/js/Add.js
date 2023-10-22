"use strict";
$(document).ready(() => {
    let counter = ($(".ingredients").children("label").length / 2);
    console.log($(".ingredients").children("label").length);
    $("#addIngredient").click(() => {
        $(".ingredients").append($('<br />'));
        $(".ingredients").append($('<label>Amount<input name="RecipeIngredients[' + counter.toString() + '].Amount" /></label>'));
        $(".ingredients").append($('<label>Ingredient<input name="Ingredients[' + counter.toString() + '].IngredientName" /></label>'));
        counter++;
    });
    $("#removeIngredient").click(() => {
        if (counter > 1) {
            $(".ingredients").children("label:last").remove();
            $(".ingredients").children("label:last").remove();
            $(".ingredients").children("br:last").remove();
            counter--;
        }       
    });
});