namespace allspice.Repositories;



public class RecipeRepository(IDbConnection db)
{
    private readonly IDbConnection _db = db;

    public Recipe CreateRecipe(Recipe recipeData)
    {
        string sql = @"
        INSERT INTO recipes
        (title, instructions, img, category, creator_id);

        SELECT
         albums.*,
         accounts.*
         FROM recipes
         JOIN accounts ON account.id = albums.creator_id
         WHERE albums.id = LAST_INSERT_ID()
        ;";

        Recipe recipe = _db.Query(sql,
        (Recipe recipe, Account account) =>
        {
            recipe.Creator = account;
            return recipe;
        }
        , recipeData).SingleOrDefault();
        return recipe;
    }
}