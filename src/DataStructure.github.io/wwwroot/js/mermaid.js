(() => {
    let renderQueued = false;

    const render = async () => {
        renderQueued = false;

        if (!window.mermaid) {
            return;
        }

        const diagrams = Array.from(
            document.querySelectorAll(".mermaid-diagram:not([data-mermaid-rendered])"));

        if (!diagrams.length) {
            return;
        }

        // Mark the nodes before rendering so the MutationObserver does not
        // start a second render while Mermaid is replacing their contents.
        for (const element of diagrams) {
            element.dataset.mermaidRendered = "true";
        }

        try {
            await window.mermaid.run({ nodes: diagrams });
        } catch (error) {
            for (const element of diagrams) {
                delete element.dataset.mermaidRendered;
            }

            console.error("Failed to render Mermaid diagram.", error);
        }
    };

    const queueRender = () => {
        if (renderQueued) {
            return;
        }

        renderQueued = true;
        queueMicrotask(() => void render());
    };

    window.renderMermaidDiagrams = queueRender;

    const observer = new MutationObserver(() => {
        queueRender();
    });

    const start = () => {
        observer.observe(document.body, {
            childList: true,
            subtree: true
        });

        queueRender();
    };

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", start, { once: true });
    } else {
        start();
    }
})();