import { ImportInfo } from "../domain/import-info";
import { ModelImportInfo } from "./model-import-info";

export class ModelImportInfoConverter{
    public static GetModelImportInfo(domain:ImportInfo):ModelImportInfo{
        return {
            filePath: domain.filePath,
            type: domain.type
        }
    }
}