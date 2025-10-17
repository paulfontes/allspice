
namespace allspice.Repositories;



public class RecipeRepository(IDbConnection db)
{
    private readonly IDbConnection _db = db;

    public Recipe CreateRecipe(Recipe recipeData)
    {
        string sql = @"
        INSERT INTO recipes
        (title, instructions, img, category, creator_id)

        VALUES
        (@Title, @Instructions, @Img, @Category, @CreatorId);

        SELECT
         recipes.*,
         accounts.*
         FROM recipes
         JOIN accounts ON accounts.id = recipes.creator_id
         WHERE recipes.id = LAST_INSERT_ID()
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

    public List<Recipe> GetAllRecipes()
    {
        string sql = @"
        SELECT
            recipes.*,
            accounts.*
        FROM 
            recipes
        INNER JOIN accounts ON accounts.id = recipes.creator_id
        ;";

        List<Recipe> recipes = _db.Query<Recipe, Account, Recipe>(sql, PopulateCreator).ToList();
        return recipes;
    }

    public Recipe GetRecipeById(int recipeId)
    {
        string sql = @"
        SELECT
        recipes.*,
        accounts.*
        FROM recipes
        INNER JOIN accounts ON accounts.id = recipes.creator_id
        WHERE recipes.id = @recipeId
        ;";

        Recipe recipe = _db.Query<Recipe, Account, Recipe>(sql, PopulateCreator, new { recipeId }).SingleOrDefault();
        return recipe;
    }

    public void UpdateRecipe(Recipe recipeData)
    {
        string sql = @"
        UPDATE recipes
        SET
        title = @Title,
        instructions = @Instructions,
        img = @Img,
        category = @Category
        WHERE id = @Id LIMIT 1;";

        int rowsAffected = _db.Execute(sql, recipeData);

        if (rowsAffected != 1)
        {
            throw new Exception($"{rowsAffected} rows of data are now updated without consent not good!");
        }
    }


    private Recipe PopulateCreator(Recipe recipe, Account creator)
    {
        recipe.Creator = creator;
        return recipe;
    }


}