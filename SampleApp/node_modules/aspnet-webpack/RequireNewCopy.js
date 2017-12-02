"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
function requireNewCopy(moduleNameOrPath) {
    // Store a reference to whatever's in the 'require' cache,
    // so we don't permanently destroy it, and then ensure there's
    // no cache entry for this module
    var resolvedModule = require.resolve(moduleNameOrPath);
    var wasCached = resolvedModule in require.cache;
    var cachedInstance;
    if (wasCached) {
        cachedInstance = require.cache[resolvedModule];
        delete require.cache[resolvedModule];
    }
    try {
        // Return a new copy
        return require(resolvedModule);
    }
    finally {
        // Restore the cached entry, if any
        if (wasCached) {
            require.cache[resolvedModule] = cachedInstance;
        }
    }
}
exports.requireNewCopy = requireNewCopy;
