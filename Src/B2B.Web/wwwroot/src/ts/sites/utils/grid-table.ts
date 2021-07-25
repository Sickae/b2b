import {Grid} from "gridjs";
import "gridjs/dist/theme/mermaid.css";
import {SearchConfig} from "gridjs/dist/src/view/plugin/search/search";
import {ServerStorageOptions} from "gridjs/dist/src/storage/server";
import SiteConfig from "./siteconfig";
import {TData} from "gridjs/dist/src/types";
import {docReady} from "./docready";
import {PaginationConfig} from "gridjs/dist/src/view/plugin/pagination";
import GridConfig = GridTable.GridConfig;

// these values will be updated with the real values using the temporary value as key
let GRID_MODEL: {[p: string]: string} = {
    Search: 'grid-model-search',
    PageIndex: 'grid-model-page-index',
    PaginationLimit: 'grid-model-pagination-limit',
};
let keysToAssert: string[] = [];

docReady(() => {
    updateGridModelValues();
});

export module GridTable {
    export interface GridConfig {
        data?: any[][] | null;
        isSearchEnabled?: boolean;
        isPaginationEnabled?: boolean;
        isSortEnabled?: boolean;
        isServerSideData?: boolean;
        isServerSideSearch?: boolean;
        isServerSidePagination?: boolean;
        isServerSideSort?: boolean;
        serverUrl?: string;
        columnsMap?: {[p: string]: string},
        paginationLimit?: number,
    }

    export function createGrid(config: GridConfig): Grid {
        SiteConfig.assertKeys(keysToAssert);
        return new Grid({
            columns: setupColumns(config),
            search: setupSearch(config),
            server: setupServer(config),
            data: setupClientSideData(config),
            pagination: setupPagination(config),
        });
    }
}

export default {GridTable};

interface GridData {
    result: any[];
    totalCount: number;
}

function setupColumns(config: GridConfig): string[] | undefined  {
    if (config.columnsMap != null)
        return Object.keys(config.columnsMap);
}

function setupServer(config: GridConfig) : ServerStorageOptions | undefined {
    if (config.isServerSideData === true && config.serverUrl != null)
        return {
            url: config.serverUrl,
            method: 'POST',
            body: createBody(),
            then: (data: GridData) => mapServerSideData(data, config),
            total: (data: GridData) => data.totalCount,
        }
}

function mapServerSideData(data: GridData, config: GridConfig): any[][] {
    if (config.columnsMap == null)
        return [];

    return data.result.map((row: any) => {
        return Object.keys(config.columnsMap!).map((dataKey: string) => row[config.columnsMap![dataKey]]);
    });
}

function setupSearch(config: GridConfig): boolean | SearchConfig | undefined {
    if (config.isSearchEnabled === true) {
        if (config.isServerSideSearch !== true)
            return true;

        return {
            server: {
                url: (prevUrl, keyword) => {
                    console.log(keyword);
                    return prevUrl;
                },
                body: (body: BodyInit, keyword: string) => {
                    console.log(keyword);
                    let formData = body as FormData;
                    formData.set(GRID_MODEL.Search, keyword);
                    return formData;
                }
            }
        }
    }
}

function setupClientSideData(config: GridConfig): TData | undefined {
    if (config.data != null)
        return config.data;
}

function setupPagination(config: GridConfig): PaginationConfig | boolean {
    if (config.isPaginationEnabled === true) {
        if (config.isServerSidePagination === true) {
            return {
                enabled: true,
                limit: config.paginationLimit ?? 10,
                server: {
                    body: (body: BodyInit, page: number, limit: number) => {
                        let formData = body as FormData;
                        formData.set(GRID_MODEL.PageIndex, page.toString());
                        formData.set(GRID_MODEL.PaginationLimit, limit.toString());
                        return formData;
                    }
                }
            }
        }

        return true;
    }

    return false;
}

function createBody(): BodyInit {
    let formData = new FormData();
    formData.append(GRID_MODEL.Search, '');
    return formData;
}

function updateGridModelValues(): void {
    // clear the assert keys first
    keysToAssert.length = 0;
    for (let key of Object.keys(GRID_MODEL)) {
        keysToAssert.push(GRID_MODEL[key]);
        GRID_MODEL[key] = SiteConfig.getValue(GRID_MODEL[key]);
    }
}
