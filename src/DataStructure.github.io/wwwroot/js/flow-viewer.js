const instances = new WeakMap();

function decodeLabel(value) {
    return (value ?? "")
        .replaceAll("&quot;", '"')
        .replaceAll("&#39;", "'")
        .replaceAll("&amp;", "&")
        .replace(/"(.+?)"$/s, "$1")
        .trim();
}

function parseGraph(source) {
    const nodes = new Map();
    const edges = [];

    const addNode = (id, label = id, shape = "round-rectangle") => {
        if (!id || id === "flowchart" || id === "graph")
            return;

        const existing = nodes.get(id);
        nodes.set(id, {
            data: {
                id,
                label: (existing?.data.label ?? decodeLabel(label)) || id,
                shape: existing?.data.shape ?? shape
            }
        });
    };

    const nodePattern = /([A-Za-z_][\w-]*)\s*(\[([^\]]*)\]|\{([^}]*)\}|\(([^)]*)\))/g;

    for (const match of source.matchAll(nodePattern)) {
        const label = match[3] ?? match[4] ?? match[5] ?? match[1];
        const shape = match[4] !== undefined
            ? "diamond"
            : match[5] !== undefined
                ? "ellipse"
                : "round-rectangle";

        addNode(match[1], label, shape);
    }

    const edgePattern = /([A-Za-z_][\w-]*)\s*(?:\[[^\]]*\]|\{[^}]*\}|\([^)]*\))?\s*(?:--\s*"([^"]*)"\s*-->|-\.\s*"([^"]*)"\s*\.->|-->\|([^|]*)\||-->|-\.->|==>)\s*([A-Za-z_][\w-]*)(?:\[[^\]]*\]|\{[^}]*\}|\([^)]*\))?/g;

    for (const match of source.matchAll(edgePattern)) {
        const sourceId = match[1];
        const label = match[2] ?? match[3] ?? match[4] ?? "";
        const targetId = match[5];

        if (!targetId)
            continue;

        addNode(sourceId);
        addNode(targetId);

        edges.push({
            data: {
                id: "edge-" + edges.length,
                source: sourceId,
                target: targetId,
                label: decodeLabel(label)
            }
        });
    }

    return [...nodes.values(), ...edges];
}

function decorateFlow(cy) {
    const nodes = cy.nodes();
    const stepById = new Map();
    const remaining = new Set(nodes.map(node => node.id()));
    const queue = [];

    // A flow starts at nodes without incoming edges. In a disconnected graph,
    // each independent source is treated as the beginning of its own flow.
    nodes.forEach(node => {
        if (node.incomers("edge").length === 0)
            queue.push(node);
    });

    // Keep disconnected/cyclic graphs deterministic.
    if (queue.length === 0 && nodes.length > 0)
        queue.push(nodes[0]);

    let nextStep = 1;

    while (queue.length > 0) {
        const node = queue.shift();

        if (!remaining.has(node.id()))
            continue;

        remaining.delete(node.id());
        stepById.set(node.id(), nextStep++);

        node.outgoers("node").forEach(target => {
            if (remaining.has(target.id()))
                queue.push(target);
        });
    }

    // Cycles and isolated components without a discovered source still need
    // an orientation number.
    while (remaining.size > 0) {
        const nodeId = [...remaining][0];
        const node = cy.getElementById(nodeId);

        remaining.delete(nodeId);
        stepById.set(nodeId, nextStep++);

        node.outgoers("node").forEach(target => {
            if (!stepById.has(target.id()) && remaining.has(target.id()))
                queue.push(target);
        });

        while (queue.length > 0) {
            const queued = queue.shift();

            if (!remaining.has(queued.id()))
                continue;

            remaining.delete(queued.id());
            stepById.set(queued.id(), nextStep++);

            queued.outgoers("node").forEach(target => {
                if (remaining.has(target.id()))
                    queue.push(target);
            });
        }
    }

    nodes.forEach(node => {
        const step = stepById.get(node.id());
        const label = node.data("label");

        node.data("step", step);
        node.data("displayLabel", step ? `${step}. ${label}` : label);

        if (node.incomers("edge").length === 0)
            node.addClass("flow-viewer-start");

        if (node.outgoers("edge").length === 0)
            node.addClass("flow-viewer-end");
    });
}

function createLayout(cy, animate = true) {
    return cy.layout({
        name: "cola",
        animate,
        maxSimulationTime: 1800,
        fit: true,
        padding: 55,
        nodeSpacing: 70,
        edgeLength: 150,
        randomize: true,
        ungrabifyWhileSimulating: false,
        nodeDimensionsIncludeLabels: true
    });
}

function initializeCanvas(canvas) {
    if (!window.cytoscape)
        throw new Error("Cytoscape.js is not loaded.");

    const sourceElement = canvas.parentElement.querySelector(".flow-viewer-source");
    const source = sourceElement?.textContent?.trim() ?? "";
    const elements = parseGraph(source);

    const cy = window.cytoscape({
        container: canvas,
        elements,
        wheelSensitivity: 0.05,
        minZoom: 0.2,
        maxZoom: 3,
        boxSelectionEnabled: false,
        autoungrabify: false,
        style: [
            {
                selector: "node",
                style: {
                    label: "data(displayLabel)",
                    shape: "data(shape)",
                    "background-color": "#2b2b40",
                    "border-color": "#594ae2",
                    "border-width": 2,
                    color: "#ffffff",
                    "font-family": "Roboto, sans-serif",
                    "font-size": 13,
                    "font-weight": 500,
                    "text-valign": "center",
                    "text-halign": "center",
                    width: "label",
                    height: "label",
                    padding: 14,
                    "text-wrap": "wrap",
                    "text-max-width": 180
                }
            },
            {
                selector: "node.flow-viewer-start",
                style: {
                    "background-color": "#173d31",
                    "border-color": "#00e676",
                    "border-width": 4,
                    "overlay-color": "#00e676",
                    "overlay-opacity": 0.12,
                    "overlay-padding": 6
                }
            },
            {
                selector: "node.flow-viewer-end",
                style: {
                    "background-color": "#493719",
                    "border-color": "#ffb300",
                    "border-width": 4,
                    "overlay-color": "#ffb300",
                    "overlay-opacity": 0.12,
                    "overlay-padding": 6
                }
            },
            {
                selector: "node.flow-viewer-selected",
                style: {
                    "background-color": "#594ae2",
                    "border-color": "#00e676",
                    "border-width": 4,
                    "overlay-color": "#00e676",
                    "overlay-opacity": 0.15,
                    "overlay-padding": 5
                }
            },
            {
                selector: "edge",
                style: {
                    width: 2,
                    "line-color": "#594ae2",
                    "target-arrow-color": "#00e676",
                    "target-arrow-shape": "triangle",
                    "curve-style": "bezier",
                    "control-point-step-size": 40,
                    label: "data(label)",
                    color: "#7777a0",
                    "font-size": 11,
                    "text-background-color": "#17172a",
                    "text-background-opacity": 0.9,
                    "text-background-padding": 3,
                    "text-rotation": "autorotate"
                }
            }
        ]
    });

    cy.ready(() => {
        if (elements.length === 0)
            return;

        decorateFlow(cy);
        createLayout(cy).run();
    });

    cy.on("tap", "node", event => {
        cy.nodes().removeClass("flow-viewer-selected");
        event.target.addClass("flow-viewer-selected");
    });

    instances.set(canvas, cy);
    return cy;
}

export function initialize(canvas) {
    if (instances.has(canvas))
        return;

    initializeCanvas(canvas);
}

export function resetLayout(canvas) {
    const cy = instances.get(canvas);
    if (!cy)
        return;

    createLayout(cy).run();
}

export function fit(canvas) {
    const cy = instances.get(canvas);
    if (!cy)
        return;

    cy.fit(undefined, 55);
}

export function dispose(canvas) {
    const cy = instances.get(canvas);
    if (!cy)
        return;

    cy.destroy();
    instances.delete(canvas);
}
