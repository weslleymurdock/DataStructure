(() => {
    const render = async () => {
        if (!window.mermaid) {
            return;
        }

        const diagrams = Array.from(
            document.querySelectorAll(".mermaid-diagram:not([data-rendered])"));

        if (!diagrams.length) {
            return;
        }

        for (const element of diagrams) {
            const source = element.dataset.mermaid;

            if (!source) {
                continue;
            }

            element.removeAttribute("data-rendered");
            element.textContent = source;
        }

        try {
            await window.mermaid.run({ nodes: diagrams });

            for (const element of diagrams) {
                element.dataset.rendered = "true";
            }
        } catch (error) {
            console.error("Failed to render Mermaid diagram.", error);
        }
    };

    window.renderMermaidDiagrams = render;

    const observer = new MutationObserver(() => {
        void render();
    });

    const start = () => {
        observer.observe(document.body, {
            childList: true,
            subtree: true
        });

        void render();
    };

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", start, { once: true });
    } else {
        start();
    }
})();