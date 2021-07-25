import docReady from "./docready";

class SiteConfigHandler {
    private _configs: {[p: string]: string} = {};
    private _cfgMarkerRegex = new RegExp(/cfg-(?<key>.+)_(?<value>.+)/);

    public load(): void {
        let containers = document.querySelectorAll<HTMLElement>('[data-site-config]');
        if (containers.length === 0)
            return;

        for (let container of Array.from(containers)) {
            container.querySelectorAll<HTMLInputElement>('input').forEach((el) => {
                let match = this._cfgMarkerRegex.exec(el.value ?? '');
                let groups = match?.groups;
                if (groups == null)
                    return;

                this._configs[groups.key] = groups.value;
            });
        }
    }

    public getValue(key: string): string {
        return this._configs[key];
    }

    public assertKeys(keys: string[]) {
        for (let key of keys)
            console.assert(this.getValue(key), `Key '${key}' is not present in the list.`);
    }
}

const SiteConfig: SiteConfigHandler = new SiteConfigHandler();
export default SiteConfig;

docReady(() => {
   SiteConfig.load();
});
