export module SiteConfig
{
    export interface SiteConfigHandler {
        getValue: (key: string) => string;
        assertKeys: (keys: string[]) => void;
    }

    export interface SiteConfig {
        [p: string]: string;
    }

    class SiteConfigHandlerImpl implements SiteConfigHandler {
        private _containerId: string;
        private _configs: SiteConfig = {};
        private _cfgMarkerRegex = new RegExp(/cfg-(?<key>.+)_(?<value>.+)/);

        constructor(containerId: string) {
            this._containerId = containerId;

            let siteCfgContainer = document.getElementById(containerId);
            if (siteCfgContainer === null) {
                console.error(`${containerId} container cannot be found.`);
                return;
            }

            siteCfgContainer.querySelectorAll<HTMLInputElement>('input').forEach((el) => {
                let match = this._cfgMarkerRegex.exec(el.value ?? '');
                let groups = match?.groups;
                if (groups == null)
                    return;

                this._configs[groups.key] = groups.value;
            });
        }

        public getValue(key: string): string {
            return this._configs[key];
        }

        public assertKeys(keys: string[]) {
            for (let key of keys)
                console.assert(this.getValue(key), `${key} is not present in the ${this._containerId} container.`);
        }
    }

    export function load(containerId: string) {
        return new SiteConfigHandlerImpl(containerId);
    }
}

export default SiteConfig;
