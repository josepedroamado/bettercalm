import { Category } from "../domain/category";
import { ModelCategory } from "./model-category";

export class ModelCategoryConverter{
    public static GetDomainCategory(category:ModelCategory):Category{
        return {
            id: category.id,
            name: category.name
        };
    }
}