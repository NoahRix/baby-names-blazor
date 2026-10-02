export function getMode() {
    try {
        const stored = localStorage.getItem('themeMode');
        if (stored === 'light' || stored === 'dark') {
            return stored;
        }
    } catch {
        // storage unavailable (private browsing); fall through to the system preference
    }

    return (window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches) ? 'dark' : 'light';
}

export function setMode(mode) {
    try {
        localStorage.setItem('themeMode', mode);
    } catch {
        // ignore; the in-memory mode still applies for this session
    }
    applyTheme(mode);
}

export function applyTheme(mode) {
    document.documentElement.setAttribute('data-theme', mode);
}
