import { ImportTypes } from "../domain/import-types";
import { ModelImportTypes } from "./model-import-types";

export class ModelImportTypesConverter{
    public static GetDomainImportTypes(model:ModelImportTypes):ImportTypes{
        return {
            types: model.types
        };
    }
}