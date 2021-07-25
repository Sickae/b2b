import {GridTable} from "./utils/grid-table";
import SiteConfig from "./utils/siteconfig";
import {docReady} from "./utils/docready";

docReady(() => {
    SiteConfig.load();
    const wrapper = document.querySelector<Element>('#table-wrapper');
    const gridConfig: GridTable.GridConfig = {
        columnsMap: {
            Name: 'name',
            Email: 'email',
        },
        serverUrl: SiteConfig.getValue('table-url'),
        isServerSideData: true,
        isSearchEnabled: true,
        isServerSideSearch: true,
        isPaginationEnabled: true,
        isServerSidePagination: true,
        paginationLimit: 5
    };
    const grid = GridTable.createGrid(gridConfig);

    if (wrapper !== null)
        grid.render(wrapper);
});
