namespace DataStructure.github.io.Services;

public sealed record MethodGuide(string Name,string Signature,string Explanation,string Complexity);

public sealed record StructureGuide(
    string Key,
    string Category,
    string TypeName,
    string Name,
    string Description,
    string UseCase,
    string Source,
    IReadOnlyList<MethodGuide> Methods)
{
    public string Utility =>
        Key switch
        {
            "maxheap" =>
                "Mantém o maior elemento na raiz, permitindo consultas imediatas ao máximo e remoções em tempo logarítmico.",
            "hashtable" =>
                "Mapeia cada chave para um bucket usando seu hash code, resolvendo colisões por encadeamento.",
            "weightedgraph" =>
                "Mantém listas de adjacência com pesos não negativos para algoritmos de caminhos mínimos.",
            _ => UseCase
        };

    public string Mermaid =>
        Key switch
        {
            "array" => @"flowchart LR
    A[""_items: T[]""]
    A --> B[""index 0""]
    A --> C[""index 1""]
    A --> D[""index n-1""]
    B -. ""contiguous"" .- C
    C -. ""contiguous"" .- D",
            "collection" => @"flowchart TD
    A[""_items: T[] + _count""] --> B{""_count < capacity?""}
    B -- ""yes"" --> C[""store at _items[_count]""]
    C --> D[""_count++""]
    B -- ""no"" --> E[""double capacity""]
    E --> F[""Array.Resize""]
    F --> C",
            "list" => @"flowchart TD
    A[""DSList""] --> B[""DSCollection backing array""]
    B --> C[""index access O(1)""]
    B --> D[""Insert / RemoveAt""]
    D --> E[""shift suffix""]
    E --> F[""update _count""]",
            "linkedlist" => @"flowchart LR
    H[""Head""] <--> A[""Node 1""]
    A <--> B[""Node 2""]
    B <--> C[""Node n""]
    C <--> T[""Tail""]
    A -. ""Previous"" .-> H
    C -. ""Next = null"" .-> X[""end""]",
            "nodelist" => @"flowchart LR
    H[""Head""] --> A[""Node 1""] --> B[""Node 2""] --> C[""Node n""]
    C --> T[""Tail""]",
            "circularlinkedlist" => @"flowchart LR
    H[""Head""] --> A[""Node 1""] --> B[""Node 2""] --> T[""Tail""]
    T --> H
    N[""tail.Next = head""] -.-> T",
            "queue" => @"flowchart LR
    H[""Head / Front""] --> A[""oldest""]
    A --> B[""next""]
    B --> T[""Tail / newest""]
    E[""Enqueue""] --> T
    D[""Dequeue""] --> H",
            "stack" => @"flowchart TD
    T[""Top""] --> A[""newest""]
    A --> B[""older""]
    B --> C[""oldest / base""]
    P[""Push""] --> T
    O[""Pop / Peek""] --> T",
            "deque" => @"flowchart LR
    H[""Head / Front""] <--> A[""Node""]
    A <--> B[""Node""]
    B <--> T[""Tail / Rear""]
    F[""AddFirst / RemoveFirst""] --> H
    L[""AddLast / RemoveLast""] --> T",
            "deck" => @"flowchart LR
    H[""Head""] <--> A[""Node""]
    A <--> B[""Node""]
    B <--> T[""Tail""]
    F[""AddFirst / RemoveFirst""] --> H
    L[""AddLast / RemoveLast""] --> T",
            "priorityqueue" => @"flowchart TD
    R[""heap[0]: minimum""] --> L[""heap[1]""]
    R --> Q[""heap[2]""]
    L --> A[""heap[3]""]
    L --> B[""heap[4]""]
    Q --> C[""heap[5]""]
    Q --> D[""heap[6]""]
    P[""parent = (i-1)/2""] -.-> L
    K[""children = 2i+1, 2i+2""] -.-> Q",
            "binarytree" => @"flowchart TD
    R[""Root""] --> L[""Left subtree""]
    R --> Q[""Right subtree""]
    L --> LL[""Left child""]
    L --> LR[""Right child""]
    Q --> RL[""Left child""]
    Q --> RR[""Right child""]",
            "binarysearchtree" => @"flowchart TD
    R[""Root""] --> C{""value < current?""}
    C -- ""yes"" --> L[""current.Left: smaller""]
    C -- ""no / equal"" --> Q[""current.Right: greater or equal""]
    L --> C
    Q --> C",
            "heap" => @"flowchart TD
    R[""heap[0]: minimum""] --> L[""heap[1]""]
    R --> Q[""heap[2]""]
    L --> A[""heap[3]""]
    L --> B[""heap[4]""]
    Q --> C[""heap[5]""]
    Q --> D[""heap[6]""]
    I[""complete tree stored in List<T>""] -.-> R",
            "maxheap" => @"flowchart TD
    R[""heap[0]: maximum""] --> L[""heap[1]""]
    R --> Q[""heap[2]""]
    L --> A[""heap[3]""]
    L --> B[""heap[4]""]
    Q --> C[""heap[5]""]
    Q --> D[""heap[6]""]
    I[""parent >= children""] -.-> R",
            "hashtable" => @"flowchart LR
    K[""key""] --> H[""GetHashCode()""] --> M[""bucket index = hash % capacity""]
    M --> B[""bucket""]
    B --> E1[""Entry: key/value""]
    B --> E2[""Entry: key/value""]
    E1 -. ""collision chain"" .-> E2
    R[""load factor > 0.75""] --> X[""resize buckets""]
    X --> Y[""rehash all entries""]",
            "weightedgraph" => @"flowchart LR
    A[""A""] -->|2| B[""B""]
    A -->|5| C[""C""]
    B -->|3| D[""D""]
    C -->|1| D
    E[""_adjacency: weighted lists""] -.-> A
    E -.-> B
    E -.-> C
    E -.-> D",
            "graph" => @"flowchart LR
    A[""A""] --> B[""B""]
    A --> C[""C""]
    B --> D[""D""]
    C --> D
    E[""_adjacency: Dictionary<T, HashSet<T>>""] -.-> A
    E -.-> B
    E -.-> C
    E -.-> D",
            _ => @"flowchart TD
    A[""Structure""] --> B[""Storage""]
    B --> C[""Elements""]"
        };

    public string GetMethodMermaid(MethodGuide method) =>
        Key switch
        {
            "array" => method.Name switch
            {
                "Construtor" => @"flowchart TD
    A[""length""] --> B[""new T[length]""]
    B --> C[""fixed contiguous storage""]",
                "Count" => @"flowchart LR
    A[""_items.Length""] --> B[""Count""]",
                "indexer" => @"flowchart TD
    A[""index""] --> B[""T[] indexer""]
    B --> C[""_items[index]""]",
                _ => @"flowchart LR
    A[""Enumerator""] --> B[""next array position""] --> C[""value""]"
            },
            "collection" => method.Name switch
            {
                "Construtor" => @"flowchart TD
    A[""capacity""] --> B{""capacity = 0?""}
    B -- ""yes"" --> C[""_items = []""]
    B -- ""no"" --> D[""new T[capacity]""]
    C --> E[""_count = 0""]
    D --> E",
                "Add" => @"flowchart TD
    A[""Add(item)""] --> B[""EnsureCapacity(_count + 1)""]
    B --> C[""_items[_count] = item""]
    C --> D[""_count++""]
    B --> E{""required > length?""}
    E -- ""yes"" --> F[""double capacity""]
    F --> G[""Array.Resize""]
    G --> C
    E -- ""no"" --> C",
                "Clear" => @"flowchart TD
    A[""Clear()""] --> B[""Array.Clear(_items, 0, _count)""]
    B --> C[""_count = 0""]",
                "Contains" => @"flowchart TD
    A[""Contains(item)""] --> B[""IndexOf(item)""]
    B --> C{""match?""}
    C -- ""yes"" --> D[""index >= 0: true""]
    C -- ""no"" --> E[""-1: false""]",
                "CopyTo" => @"flowchart TD
    A[""CopyTo(array, arrayIndex)""] --> B[""validate destination""]
    B --> C[""Array.Copy(_items, 0, array, arrayIndex, _count)""]",
                "Remove" => @"flowchart TD
    A[""Remove(item)""] --> B[""IndexOf(item)""]
    B --> C{""found?""}
    C -- ""no"" --> D[""false""]
    C -- ""yes"" --> E[""RemoveAt(index)""]
    E --> F[""true""]",
                "indexer" => @"flowchart TD
    A[""index""] --> B[""ValidateIndex""]
    B --> C[""_items[index] get/set""]",
                "IndexOf" => @"flowchart TD
    A[""index = 0""] --> B{""index < _count?""}
    B -- ""yes"" --> C{""items[index] == target?""}
    C -- ""yes"" --> D[""return index""]
    C -- ""no"" --> E[""index++""]
    E --> B
    B -- ""no"" --> F[""return -1""]",
                "InsertAt" => @"flowchart TD
    A[""InsertAt(index,item)""] --> B[""EnsureCapacity""]
    B --> C[""Array.Copy suffix right by 1""]
    C --> D[""_items[index] = item""]
    D --> E[""_count++""]",
                "RemoveAt" => @"flowchart TD
    A[""RemoveAt(index)""] --> B[""ValidateIndex""]
    B --> C[""save _items[index]""]
    C --> D[""Array.Copy suffix left by 1""]
    D --> E[""_items[--_count] = default""]
    E --> F[""return value""]",
                "EnsureCapacity" => @"flowchart TD
    A[""required""] --> B{""required <= length?""}
    B -- ""yes"" --> C[""return""]
    B -- ""no"" --> D[""double capacity until enough""]
    D --> E[""Array.Resize""]
    E --> F[""return"" ]",
                _ => @"flowchart LR
    A[""Enumerator""] --> B[""index 0.._count-1""] --> C[""yield value""]"
            },
            "list" => method.Name switch
            {
                "Insert" => @"flowchart TD
    A[""Insert(index,item)""] --> B[""InsertAt""]
    B --> C[""EnsureCapacity""]
    C --> D[""shift suffix right""]
    D --> E[""write item""]
    E --> F[""_count++""]",
                "RemoveAt" => @"flowchart TD
    A[""RemoveAt(index)""] --> B[""base.RemoveAt""]
    B --> C[""shift suffix left""]
    C --> D[""decrement count""]
    D --> E[""return removed value""]",
                "IndexOf" => @"flowchart TD
    A[""index = 0""] --> B{""index < Count?""}
    B -- ""yes"" --> C{""this[index] == item?""}
    C -- ""yes"" --> D[""return index""]
    C -- ""no"" --> E[""index++""]
    E --> B
    B -- ""no"" --> F[""return -1""]",
                "Contains" => @"flowchart TD
    A[""Contains(item)""] --> B[""IndexOf(item)""]
    B --> C{""index >= 0?""}
    C -- ""yes"" --> D[""true""]
    C -- ""no"" --> E[""false""]",
                _ => Mermaid
            },
            "linkedlist" => method.Name switch
            {
                "AddFirst" => @"flowchart TD
    A[""new node""] --> B{""head is null?""}
    B -- ""yes"" --> C[""head = tail = node""]
    B -- ""no"" --> D[""node.Next = head""]
    D --> E[""head.Previous = node""]
    E --> F[""head = node""]
    C --> G[""Count++""]
    F --> G",
                "AddLast" => @"flowchart TD
    A[""new node""] --> B{""tail is null?""}
    B -- ""yes"" --> C[""head = tail = node""]
    B -- ""no"" --> D[""node.Previous = tail""]
    D --> E[""tail.Next = node""]
    E --> F[""tail = node""]
    C --> G[""Count++""]
    F --> G",
                "RemoveFirst" => @"flowchart TD
    A[""head""] --> B[""save value""]
    B --> C[""head = head.Next""]
    C --> D{""head is null?""}
    D -- ""yes"" --> E[""tail = null""]
    D -- ""no"" --> F[""head.Previous = null""]
    E --> G[""Count-- / return""]
    F --> G",
                "RemoveLast" => @"flowchart TD
    A[""tail""] --> B[""save value""]
    B --> C[""tail = tail.Previous""]
    C --> D{""tail is null?""}
    D -- ""yes"" --> E[""head = null""]
    D -- ""no"" --> F[""tail.Next = null""]
    E --> G[""Count-- / return""]
    F --> G",
                "Insert" => @"flowchart TD
    A[""Insert(index,item)""] --> B{""index = 0?""}
    B -- ""yes"" --> C[""AddFirst""]
    B -- ""no"" --> D{""index = Count?""}
    D -- ""yes"" --> E[""AddLast""]
    D -- ""no"" --> F[""GetNode(index)""]
    F --> G[""node.Previous = current.Previous""]
    G --> H[""node.Next = current""]
    H --> I[""previous.Next = node""]
    I --> J[""current.Previous = node""]
    J --> K[""Count++""]",
                "Remove" => @"flowchart TD
    A[""current = head""] --> B{""current != null?""}
    B -- ""yes"" --> C{""current.Value == item?""}
    C -- ""no"" --> D[""current = current.Next""] --> B
    C -- ""yes"" --> E{""head / tail / middle?""}
    E --> F[""unlink or delegate to RemoveFirst/RemoveLast""]
    F --> G[""Count-- / true""]
    B -- ""no"" --> H[""false""]",
                _ => @"flowchart TD
    A[""GetNode(index)""] --> B{""index < Count / 2?""}
    B -- ""yes"" --> C[""walk Next from head""]
    B -- ""no"" --> D[""walk Previous from tail""]
    C --> E[""return node""]
    D --> E"
            },
            "nodelist" => method.Name switch
            {
                "Add" => @"flowchart TD
    A[""new node""] --> B{""head null?""}
    B -- ""yes"" --> C[""head = tail = node""]
    B -- ""no"" --> D[""tail.Next = node""]
    D --> E[""tail = node""]
    C --> F[""Count++""]
    E --> F",
                "Remove" => @"flowchart TD
    A[""head""] --> B{""head matches?""}
    B -- ""yes"" --> C[""head = head.Next""]
    B -- ""no"" --> D[""walk current.Next""]
    D --> E{""next matches?""}
    E -- ""no"" --> D
    E -- ""yes"" --> F[""current.Next = current.Next.Next""]
    F --> G{""tail removed?""}
    G -- ""yes"" --> H[""tail = current""]
    G -- ""no"" --> I[""keep tail""]
    C --> J[""Count--""]
    H --> J
    I --> J",
                _ => @"flowchart TD
    A[""index""] --> B[""current = head""]
    B --> C[""advance Next until index""]
    C --> D[""return current.Value""]"
            },
            "circularlinkedlist" => method.Name switch
            {
                "AddFirst" => @"flowchart TD
    A[""new node""] --> B{""empty?""}
    B -- ""yes"" --> C[""head = tail = node; node.Next = node""]
    B -- ""no"" --> D[""node.Next = head""]
    D --> E[""head = node""]
    E --> F[""tail.Next = head""]
    C --> G[""Count++""]
    F --> G",
                "AddLast" => @"flowchart TD
    A[""new node""] --> B{""empty?""}
    B -- ""yes"" --> C[""head = tail = node; node.Next = node""]
    B -- ""no"" --> D[""node.Next = head""]
    D --> E[""tail.Next = node""]
    E --> F[""tail = node""]
    C --> G[""Count++""]
    F --> G",
                "RemoveFirst" => @"flowchart TD
    A[""head""] --> B{""Count = 1?""}
    B -- ""yes"" --> C[""head = tail = null""]
    B -- ""no"" --> D[""head = head.Next""]
    D --> E[""tail.Next = head""]
    C --> F[""Count-- / return""]
    E --> F",
                "RemoveLast" => @"flowchart TD
    A[""tail""] --> B{""Count = 1?""}
    B -- ""yes"" --> C[""head = tail = null""]
    B -- ""no"" --> D[""walk until current.Next = tail""]
    D --> E[""current.Next = head""]
    E --> F[""tail = current""]
    C --> G[""Count-- / return""]
    F --> G",
                "Contains" => @"flowchart TD
    A[""current = head""] --> B[""compare current.Value""]
    B --> C{""match?""}
    C -- ""yes"" --> D[""true""]
    C -- ""no"" --> E[""current = current.Next""]
    E --> F{""back at head?""}
    F -- ""no"" --> B
    F -- ""yes"" --> G[""false""]",
                _ => @"flowchart TD
    A[""current = head""] --> B[""yield value""]
    B --> C[""current = current.Next""]
    C --> D{""current == head?""}
    D -- ""no"" --> B
    D -- ""yes"" --> E[""stop""]"
            },
            "queue" => method.Name switch
            {
                "Enqueue" => @"flowchart TD
    A[""new node""] --> B{""tail null?""}
    B -- ""yes"" --> C[""head = tail = node""]
    B -- ""no"" --> D[""tail.Next = node""]
    D --> E[""tail = node""]
    C --> F[""Count++""]
    E --> F",
                "Dequeue" => @"flowchart TD
    A[""head""] --> B[""save value""]
    B --> C[""head = head.Next""]
    C --> D{""head null?""}
    D -- ""yes"" --> E[""tail = null""]
    D -- ""no"" --> F[""keep tail""]
    E --> G[""Count-- / return""]
    F --> G",
                "Peek" => @"flowchart TD
    A[""head""] --> B[""return head.Value""]",
                _ => Mermaid
            },
            "stack" => method.Name switch
            {
                "Push" => @"flowchart TD
    A[""new node""]
    A --> B[""node.Next = top""]
    B --> C[""top = node""]
    C --> D[""Count++""]",
                "Pop" => @"flowchart TD
    A[""top""] --> B[""save value""]
    B --> C[""top = top.Next""]
    C --> D[""Count-- / return""]",
                "Peek" => @"flowchart TD
    A[""top""] --> B[""return top.Value""]",
                _ => Mermaid
            },
            "deque" => method.Name switch
            {
                "AddFirst" => @"flowchart TD
    A[""new node""] --> B{""head null?""}
    B -- ""yes"" --> C[""head = tail = node""]
    B -- ""no"" --> D[""node.Next = head""]
    D --> E[""head.Previous = node""]
    E --> F[""head = node""]
    C --> G[""Count++""]
    F --> G",
                "AddLast" => @"flowchart TD
    A[""new node""] --> B{""tail null?""}
    B -- ""yes"" --> C[""head = tail = node""]
    B -- ""no"" --> D[""node.Previous = tail""]
    D --> E[""tail.Next = node""]
    E --> F[""tail = node""]
    C --> G[""Count++""]
    F --> G",
                "RemoveFirst" => @"flowchart TD
    A[""head""] --> B[""save value""]
    B --> C[""head = head.Next""]
    C --> D{""head null?""}
    D -- ""yes"" --> E[""tail = null""]
    D -- ""no"" --> F[""head.Previous = null""]
    E --> G[""Count-- / return""]
    F --> G",
                "RemoveLast" => @"flowchart TD
    A[""tail""] --> B[""save value""]
    B --> C[""tail = tail.Previous""]
    C --> D{""tail null?""}
    D -- ""yes"" --> E[""head = null""]
    D -- ""no"" --> F[""tail.Next = null""]
    E --> G[""Count-- / return""]
    F --> G",
                "PeekFirst" => @"flowchart TD
    A[""head""] --> B[""return head.Value""]",
                "PeekLast" => @"flowchart TD
    A[""tail""] --> B[""return tail.Value""]",
                _ => Mermaid
            },
            "deck" => method.Name switch
            {
                "AddFirst" => @"flowchart TD
    A[""new node""] --> B{""head null?""}
    B -- ""yes"" --> C[""head = tail = node""]
    B -- ""no"" --> D[""node.Next = head""]
    D --> E[""head.Previous = node""]
    E --> F[""head = node""]
    C --> G[""Count++""]
    F --> G",
                "AddLast" => @"flowchart TD
    A[""new node""] --> B{""tail null?""}
    B -- ""yes"" --> C[""head = tail = node""]
    B -- ""no"" --> D[""tail.Next = node""]
    D --> E[""node.Previous = tail""]
    E --> F[""tail = node""]
    C --> G[""Count++""]
    F --> G",
                "RemoveFirst" => @"flowchart TD
    A[""head""] --> B[""save value""]
    B --> C[""head = head.Next""]
    C --> D{""head null?""}
    D -- ""yes"" --> E[""tail = null""]
    D -- ""no"" --> F[""head.Previous = null""]
    E --> G[""Count-- / return""]
    F --> G",
                "RemoveLast" => @"flowchart TD
    A[""tail""] --> B[""save value""]
    B --> C[""tail = tail.Previous""]
    C --> D{""tail null?""}
    D -- ""yes"" --> E[""head = null""]
    D -- ""no"" --> F[""tail.Next = null""]
    E --> G[""Count-- / return""]
    F --> G",
                _ => Mermaid
            },
            "priorityqueue" => method.Name switch
            {
                "Enqueue" => @"flowchart TD
    A[""append to _heap""] --> B[""index = Count - 1""]
    B --> C[""parent = (index - 1) / 2""]
    C --> D{""parent <= child?""}
    D -- ""yes"" --> E[""done""]
    D -- ""no"" --> F[""swap""]
    F --> C",
                "Peek" => @"flowchart TD
    A[""_heap[0]""] --> B[""return minimum""]",
                "Dequeue" => @"flowchart TD
    A[""save _heap[0]""]
    A --> B[""remove last""]
    B --> C{""heap empty?""}
    C -- ""yes"" --> D[""return saved""]
    C -- ""no"" --> E[""_heap[0] = last""]
    E --> F[""SiftDown""]
    F --> D",
                _ => Mermaid
            },
            "binarytree" => method.Name switch
            {
                "PreOrder" => @"flowchart TD
    A[""node""] --> B{""node null?""}
    B -- ""yes"" --> C[""yield nothing""]
    B -- ""no"" --> D[""yield node.Value""]
    D --> E[""recurse Left""]
    E --> F[""recurse Right""]",
                "InOrder" => @"flowchart TD
    A[""node""] --> B{""node null?""}
    B -- ""yes"" --> C[""yield nothing""]
    B -- ""no"" --> D[""recurse Left""]
    D --> E[""yield node.Value""]
    E --> F[""recurse Right""]",
                "PostOrder" => @"flowchart TD
    A[""node""] --> B{""node null?""}
    B -- ""yes"" --> C[""yield nothing""]
    B -- ""no"" --> D[""recurse Left""]
    D --> E[""recurse Right""]
    E --> F[""yield node.Value""]",
                _ => Mermaid
            },
            "binarysearchtree" => method.Name switch
            {
                "Insert" => @"flowchart TD
    A[""value""] --> B{""Root null?""}
    B -- ""yes"" --> C[""Root = new node""]
    B -- ""no"" --> D[""compare with current""]
    D --> E{""value < current?""}
    E -- ""yes"" --> F[""go Left""]
    E -- ""no"" --> G[""go Right""]
    F --> H{""child null?""}
    G --> H
    H -- ""no"" --> D
    H -- ""yes"" --> I[""insert node / Count++""]"
                ,
                "Contains" => @"flowchart TD
    A[""current = Root""] --> B{""current null?""}
    B -- ""yes"" --> C[""false""]
    B -- ""no"" --> D[""Compare target""]
    D --> E{""comparison = 0?""}
    E -- ""yes"" --> F[""true""]
    E -- ""no"" --> G{""target < current?""}
    G -- ""yes"" --> H[""current = Left""]
    G -- ""no"" --> I[""current = Right""]
    H --> B
    I --> B",
                "InOrder" => @"flowchart TD
    A[""node""] --> B[""recurse Left""]
    B --> C[""yield value""]
    C --> D[""recurse Right""]",
                "PreOrder" => @"flowchart TD
    A[""node""] --> B[""yield value""]
    B --> C[""recurse Left""]
    C --> D[""recurse Right""]",
                "PostOrder" => @"flowchart TD
    A[""node""] --> B[""recurse Left""]
    B --> C[""recurse Right""]
    C --> D[""yield value""]",
                _ => Mermaid
            },
            "heap" => method.Name switch 
            {
                "Add" => @"flowchart TD
    A[""append at end""] --> B[""index = last""]
    B --> C[""parent = (index - 1) / 2""]
    C --> D{""parent <= child?""}
    D -- ""yes"" --> E[""done""]
    D -- ""no"" --> F[""swap""]
    F --> C",
                "Peek" => @"flowchart TD
    A[""_items[0]""] --> B[""return minimum""]",
                "Remove" => @"flowchart TD
    A[""save root minimum""] --> B[""remove last""]
    B --> C{""items remain?""}
    C -- ""no"" --> D[""return root""]
    C -- ""yes"" --> E[""move last to root""]
    E --> F[""SiftDown: choose smaller child""]
    F --> G{""child smaller than parent?""}
    G -- ""yes"" --> H[""swap and continue""]
    H --> F
    G -- ""no"" --> D",
                "AsArray" => @"flowchart LR
    A[""_items""] --> B[""IReadOnlyList<T> view""]"
            }
            ,
            "maxheap" => method.Name switch
            {
                "Add" => @"flowchart TD
    A[""append at end""] --> B[""index = last""]
    B --> C[""parent = (index - 1) / 2""]
    C --> D{""parent >= child?""}
    D -- ""yes"" --> E[""done""]
    D -- ""no"" --> F[""swap""]
    F --> C",
                "Peek" => @"flowchart TD
    A[""_items[0]""] --> B[""return maximum""]",
                "Remove" => @"flowchart TD
    A[""save root maximum""] --> B[""remove last""]
    B --> C{""items remain?""}
    C -- ""no"" --> D[""return maximum""]
    C -- ""yes"" --> E[""move last to root""]
    E --> F[""SiftDown: choose larger child""]
    F --> G{""child larger than parent?""}
    G -- ""yes"" --> H[""swap and continue""]
    H --> F
    G -- ""no"" --> D",
                "AsArray" => @"flowchart LR
    A[""_items""] --> B[""IReadOnlyList<T> view""]",
                _ => Mermaid
            },
            "hashtable" => method.Name switch
            {
                "Construtor" => @"flowchart TD
    A[""capacity""] --> B[""create List<Entry> bucket for each slot""]
    B --> C[""_count = 0""]",
                "Add" => @"flowchart TD
    A[""key""] --> B[""GetHashCode()""]
    B --> C[""bucket index""]
    C --> D[""scan bucket for duplicate key""]
    D --> E{""duplicate?""}
    E -- ""yes"" --> F[""throw ArgumentException""]
    E -- ""no"" --> G[""EnsureCapacity""]
    G --> H[""recalculate bucket after resize""]
    H --> I[""append Entry(key,value)""]
    I --> J[""_count++""]",
                "Set" => @"flowchart TD
    A[""key""] --> B[""calculate bucket""]
    B --> C[""scan chained entries""]
    C --> D{""key found?""}
    D -- ""yes"" --> E[""replace Entry value""]
    D -- ""no"" --> F[""EnsureCapacity""]
    F --> G[""append new Entry""]
    G --> H[""_count++""]",
                "TryGetValue" => @"flowchart TD
    A[""key""] --> B[""hash -> bucket index""]
    B --> C[""current entry in bucket""]
    C --> D{""current key == key?""}
    D -- ""yes"" --> E[""return value / true""]
    D -- ""no"" --> F[""next entry in chain""]
    F --> C
    C --> G[""end of bucket -> false""]",
                "ContainsKey" => @"flowchart TD
    A[""key""] --> B[""TryGetValue(key)""]
    B --> C[""true when key is found; otherwise false""]",
                "Remove" => @"flowchart TD
    A[""key""] --> B[""hash -> bucket index""]
    B --> C[""scan bucket chain""]
    C --> D{""key found?""}
    D -- ""yes"" --> E[""RemoveAt(index)""]
    E --> F[""_count-- / true""]
    D -- ""no"" --> G[""false""]",
                "Clear" => @"flowchart TD
    A[""Clear()""] --> B[""create fresh bucket array with same capacity""]
    B --> C[""_count = 0""]",
                "Enumerate" => @"flowchart TD
    A[""buckets[0]""] --> B[""iterate entries""]
    B --> C[""next bucket""]
    C --> D{""more buckets?""}
    D -- ""yes"" --> B
    D -- ""no"" --> E[""finish""]",
                _ => Mermaid
            },
            "weightedgraph" => method.Name switch
            {
                "AddVertex" => @"flowchart TD
    A[""AddVertex(vertex)""] --> B{""vertex exists?""}
    B -- ""no"" --> C[""create empty edge list""]
    B -- ""yes"" --> D[""keep existing vertex""]",
                "AddEdge" => @"flowchart TD
    A[""from, to, weight""] --> B[""validate weight >= 0""]
    B --> C[""ensure vertices""]
    C --> D{""edge to destination exists?""}
    D -- ""yes"" --> E[""replace weight""]
    D -- ""no"" --> F[""append weighted edge""]",
                "Neighbors" => @"flowchart TD
    A[""vertex""] --> B[""find adjacency list""]
    B --> C[""return destination + weight pairs""]",
                "Vertices" => @"flowchart LR
    A[""_adjacency.Keys""] --> B[""vertices""]",
                _ => @"flowchart LR
    A[""weighted graph operation""] --> B[""adjacency lists""]"
            },
            "graph" => method.Name switch
            {
                "AddVertex" => @"flowchart TD
    A[""vertex""] --> B[""_adjacency.TryAdd(vertex, empty set)""]
    B --> C[""vertex available in Vertices""]",
                "AddEdge" => @"flowchart TD
    A[""from, to""] --> B[""AddVertex(from)""]
    B --> C[""AddVertex(to)""]
    C --> D[""_adjacency[from].Add(to)""]
    D --> E[""directed edge from -> to""]",
                "HasEdge" => @"flowchart TD
    A[""from""] --> B[""TryGetValue(from)""]
    B --> C{""neighbors found?""}
    C -- ""no"" --> D[""false""]
    C -- ""yes"" --> E[""neighbors.Contains(to)""]
    E --> F[""true / false""]",
                "Neighbors" => @"flowchart TD
    A[""vertex""] --> B[""TryGetValue(vertex)""]
    B --> C{""found?""}
    C -- ""yes"" --> D[""return HashSet neighbors""]
    C -- ""no"" --> E[""return empty collection""]",
                "BreadthFirst" => @"flowchart TD
    A[""start""] --> B[""visited = {start}; enqueue start""]
    B --> C{""queue not empty?""}
    C -- ""yes"" --> D[""dequeue vertex; yield it""]
    D --> E[""for each neighbor""]
    E --> F{""visited.Add(neighbor)?""}
    F -- ""yes"" --> G[""enqueue neighbor""]
    F -- ""no"" --> E
    G --> C
    C -- ""no"" --> H[""finish"" ]",
                "DepthFirst" => @"flowchart TD
    A[""start""] --> B[""visited = empty""]
    B --> C[""DepthFirstCore(start)""]
    C --> D{""visited.Add(vertex)?""}
    D -- ""no"" --> E[""return""]
    D -- ""yes"" --> F[""yield vertex""]
    F --> G[""for each neighbor""]
    G --> H[""DepthFirstCore(neighbor)""]
    H --> G",
                _ => Mermaid
            },
            _ => Mermaid
        };

}

public static class StructureCatalog
{
    public static IReadOnlyList<StructureGuide> All { get; } =
    [
        new("array","linear","DSArray","Array","Armazenamento contíguo de tamanho fixo com acesso indexado.","Quando o tamanho é conhecido e o acesso direto por índice é importante.",@"namespace DataStructure.Abstractions;

public sealed class DSArray<T> : IReadOnlyList<T>
{
    private readonly T[] _items;

    public DSArray(int length)
    {
        if (length < 0)
            throw new ArgumentOutOfRangeException(nameof(length));

        _items = new T[length];
    }

    public DSArray(IEnumerable<T> items)
    {
        ArgumentNullException.ThrowIfNull(items);

        _items = [.. items];
    }

    public int Count => _items.Length;

    public T this[int index]
    {
        get => _items[index];
        set => _items[index] = value;
    }

    public IEnumerator<T> GetEnumerator()
        => ((IEnumerable<T>)_items).GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        => GetEnumerator();
}",
            [
                new("Construtor","public DSArray(int length)","Aloca o armazenamento inicial da estrutura.","O(n)"),
                new("Count","public int Count","Informa quantos elementos ou posições a estrutura possui.","O(1)"),
                new("indexer","public T this[int index]","Acessa o valor associado ao índice informado.","O(1) para estruturas indexadas"),
            ]),
        new("collection","linear","DSCollection","Coleção dinâmica","Coleção contígua de tamanho variável com crescimento automático.","Como coleção mutável geral quando append e acesso indexado são frequentes.",@"namespace DataStructure.Abstractions;

public class DSCollection<T> : ICollection<T>
{
    private T[] _items;
    private int _count;

    public DSCollection(int capacity = 4)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(capacity);

        _items = capacity == 0 ? [] : new T[capacity];
    }

    public int Count => _count;

    public bool IsReadOnly => false;

    public virtual void Add(T item)
    {

        EnsureCapacity(_count + 1);

        _items[_count++] = item;
    }

    public void Clear()
    {

        Array.Clear(_items, 0, _count);
        _count = 0;
    }

    public bool Contains(T item)
        => IndexOf(item) >= 0;

    public void CopyTo(T[] array, int arrayIndex)
    {
        ArgumentNullException.ThrowIfNull(array);

        if (arrayIndex < 0 ||
            arrayIndex > array.Length ||
            array.Length - arrayIndex < _count)
        {
            throw new ArgumentException(
                ""The destination array is too small."",
                nameof(array));
        }

        Array.Copy(_items, 0, array, arrayIndex, _count);
    }

    public bool Remove(T item)
    {

        var index = IndexOf(item);

        if (index < 0)
            return false;

        RemoveAt(index);
        return true;
    }

    public T this[int index]
    {
        get
        {
            ValidateIndex(index);
            return _items[index];
        }
        set
        {
            ValidateIndex(index);
            _items[index] = value;
        }
    }

    protected int IndexOf(T item)
    {
        var comparer = EqualityComparer<T>.Default;

        for (var index = 0; index < _count; index++)
        {
            if (comparer.Equals(_items[index], item))
                return index;
        }

        return -1;
    }

    protected void InsertAt(int index, T item)
    {
        if (index < 0 || index > _count)
            throw new ArgumentOutOfRangeException(nameof(index));

        EnsureCapacity(_count + 1);

        Array.Copy(
            _items,
            index,
            _items,
            index + 1,
            _count - index);

        _items[index] = item;
        _count++;
    }

    protected T RemoveAt(int index)
    {
        ValidateIndex(index);

        var value = _items[index];

        Array.Copy(
            _items,
            index + 1,
            _items,
            index,
            _count - index - 1);

        _items[--_count] = default!;

        return value;
    }

    protected void EnsureCapacity(int required)
    {
        if (required <= _items.Length)
            return;

        var capacity = _items.Length == 0 ? 4 : _items.Length;

        while (capacity < required)
            capacity *= 2;

        Array.Resize(ref _items, capacity);
    }

    private void ValidateIndex(int index)
    {

        if ((uint)index >= (uint)_count)
            throw new ArgumentOutOfRangeException(nameof(index));
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (var index = 0; index < _count; index++)
            yield return _items[index];
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        => GetEnumerator();
}",
            [
                new("Construtor","public DSArray(int length)","Aloca o armazenamento inicial da estrutura.","O(n)"),
                new("Add","public void Add(T item)","Adiciona um valor ao final da estrutura.","O(1) amortizado"),
                new("Clear","public void Clear()","Remove logicamente todos os valores armazenados.","O(n)"),
                new("Contains","public bool Contains(T item)","Percorre ou consulta a estrutura para verificar a existência do valor.","O(n) em estruturas lineares"),
                new("CopyTo","public void CopyTo(T[] array, int arrayIndex)","Copia os valores lógicos para um vetor de destino.","O(n)"),
                new("Remove","public bool Remove(T item)","Localiza o primeiro valor correspondente e o remove.","O(n)"),
                new("indexer","public T this[int index]","Acessa o valor associado ao índice informado.","O(1) para estruturas indexadas"),
                new("IndexOf","public int IndexOf(T item)","Procura linearmente a primeira ocorrência.","O(n)"),
                new("InsertAt","protected void InsertAt(int index, T item)","Abre espaço deslocando o trecho à direita.","O(n)"),
                new("RemoveAt","protected T RemoveAt(int index)","Remove uma posição e desloca o trecho seguinte.","O(n)"),
                new("EnsureCapacity","protected void EnsureCapacity(int required)","Amplia o armazenamento quando a capacidade atual não é suficiente.","O(n) no redimensionamento"),
            ]),
        new("list","linear","DSList","Lista dinâmica","Especializa a coleção dinâmica para expor inserção e remoção por índice.","Quando é necessário combinar acesso por índice com inserção e remoção.",@"namespace DataStructure.Abstractions;

public sealed class DSList<T>(int capacity = 4) : DSCollection<T>(capacity), IReadOnlyList<T>
{

    public void Insert(int index, T item)
        => InsertAt(index, item);

    public new T RemoveAt(int index)
        => base.RemoveAt(index);

    public new int IndexOf(T item)
    {
        var comparer = EqualityComparer<T>.Default;

        for (var index = 0; index < Count; index++)
        {
            if (comparer.Equals(this[index], item))
                return index;
        }

        return -1;
    }

    public new bool Contains(T item)
        => IndexOf(item) >= 0;
}",
            [
                new("Insert","public void Insert(int index, T item)","Localiza a posição e conecta ou desloca os elementos necessários.","O(n)"),
                new("RemoveAt","protected T RemoveAt(int index)","Remove uma posição e desloca o trecho seguinte.","O(n)"),
                new("IndexOf","public int IndexOf(T item)","Procura linearmente a primeira ocorrência.","O(n)"),
                new("Contains","public bool Contains(T item)","Percorre ou consulta a estrutura para verificar a existência do valor.","O(n) em estruturas lineares"),
            ]),
        new("linkedlist","linear","DSLinkedList","Lista duplamente ligada","Cada nó aponta para o anterior e o próximo.","Quando inserções e remoções nas extremidades ou após localizar um nó são importantes.",@"namespace DataStructure.Abstractions;

public sealed class DSLinkedList<T> : IReadOnlyList<T>
{
    private DSNode<T>? _head;
    private DSNode<T>? _tail;

    public int Count { get; private set; }

    public T this[int index]
        => GetNode(index).Value;

    public void AddFirst(T item)
    {
        var node = new DSNode<T>(item);

        if (_head is null)
        {

            _head = _tail = node;
        }
        else
        {

            node.Next = _head;
            _head.Previous = node;
            _head = node;
        }

        Count++;
    }

    public void AddLast(T item)
    {
        var node = new DSNode<T>(item);

        if (_tail is null)
        {
            _head = _tail = node;
        }
        else
        {

            node.Previous = _tail;
            _tail.Next = node;
            _tail = node;
        }

        Count++;
    }

    public T RemoveFirst()
    {
        if (_head is null)
            throw new InvalidOperationException(""The linked list is empty."");

        var value = _head.Value;
        _head = _head.Next;

        if (_head is null)
            _tail = null;
        else
            _head.Previous = null;

        Count--;
        return value;
    }

    public T RemoveLast()
    {
        if (_tail is null)
            throw new InvalidOperationException(""The linked list is empty."");

        var value = _tail.Value;
        _tail = _tail.Previous;

        if (_tail is null)
            _head = null;
        else
            _tail.Next = null;

        Count--;
        return value;
    }

    public void Insert(int index, T item)
    {
        if (index < 0 || index > Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        if (index == 0)
        {
            AddFirst(item);
            return;
        }

        if (index == Count)
        {
            AddLast(item);
            return;
        }

        var current = GetNode(index);
        var node = new DSNode<T>(item)
        {
            Previous = current.Previous,
            Next = current
        };

        current.Previous!.Next = node;
        current.Previous = node;
        Count++;
    }

    public bool Remove(T item)
    {
        var comparer = EqualityComparer<T>.Default;
        var current = _head;

        while (current is not null)
        {
            if (comparer.Equals(current.Value, item))
            {
                if (current.Previous is null)
                    RemoveFirst();
                else if (current.Next is null)
                    RemoveLast();
                else
                {

                    current.Previous.Next = current.Next;
                    current.Next.Previous = current.Previous;
                    Count--;
                }

                return true;
            }

            current = current.Next;
        }

        return false;
    }

    private DSNode<T> GetNode(int index)
    {
        if ((uint)index >= (uint)Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        if (index < Count / 2)
        {
            var current = _head!;

            for (var position = 0; position < index; position++)
                current = current.Next!;

            return current;
        }

        var reverse = _tail!;

        for (var position = Count - 1; position > index; position--)
            reverse = reverse.Previous!;

        return reverse;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var current = _head;

        while (current is not null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        => GetEnumerator();
}",
            [
                new("AddFirst","public void AddFirst(T item)","Insere um elemento no início e atualiza a referência de entrada.","O(1)"),
                new("AddLast","public void AddLast(T item)","Insere um elemento no final e atualiza a referência de saída.","O(1)"),
                new("RemoveFirst","public T RemoveFirst()","Remove o elemento da primeira posição e atualiza o início.","O(1)"),
                new("RemoveLast","public T RemoveLast()","Remove o elemento da última posição.","O(1) em lista duplamente ligada"),
                new("Insert","public void Insert(int index, T item)","Localiza a posição e conecta ou desloca os elementos necessários.","O(n)"),
                new("Remove","public bool Remove(T item)","Localiza o primeiro valor correspondente e o remove.","O(n)"),
                new("indexer","public T this[int index]","Acessa o valor associado ao índice informado.","O(1) para estruturas indexadas"),
            ]),
        new("nodelist","linear","DSNodeList","Lista simplesmente ligada","Cadeia unidirecional de nós com referência ao primeiro e último nó.","Para estudar encadeamento simples e o custo de percorrer uma lista.",@"namespace DataStructure.Abstractions;

public sealed class DSNodeList<T> where T : notnull
{
    private DSNode<T>? _head;
    private DSNode<T>? _tail;

    public int Count { get; private set; }

    public void Add(T item)
    {
        var node = new DSNode<T>(item);

        if (_head is null)
        {

            _head = _tail = node;
        }
        else
        {

            _tail!.Next = node;
            _tail = node;
        }

        Count++;
    }

    public T this[int index]
    {
        get
        {
            if ((uint)index >= (uint)Count)
                throw new IndexOutOfRangeException();

            var current = _head;

            for (var position = 0; position < index; position++)
                current = current!.Next;

            return current!.Value;
        }
    }

    public bool Remove(T item)
    {
        if (_head is null)
            return false;

        if (_head.Value.Equals(item))
        {

            _head = _head.Next;

            if (_head is null)
                _tail = null;

            Count--;
            return true;
        }

        var current = _head;

        while (current.Next is not null)
        {
            if (!current.Next.Value.Equals(item))
            {
                current = current.Next;
                continue;
            }

            current.Next = current.Next.Next;

            if (current.Next is null)
                _tail = current;

            Count--;
            return true;
        }

        return false;
    }
}",
            [
                new("Add","public void Add(T item)","Adiciona um valor ao final da estrutura.","O(1) amortizado"),
                new("indexer","public T this[int index]","Acessa o valor associado ao índice informado.","O(1) para estruturas indexadas"),
                new("Remove","public bool Remove(T item)","Localiza o primeiro valor correspondente e o remove.","O(n)"),
            ]),
        new("circularlinkedlist","linear","DSCircularLinkedList","Lista circular","O último nó aponta novamente para o primeiro, formando um ciclo.","Em percursos cíclicos, escalas, turnos e algoritmos que precisam retornar ao início.",@"namespace DataStructure.Abstractions;

public sealed class DSCircularLinkedList<T>
{
    private DSNode<T>? _head;
    private DSNode<T>? _tail;

    public int Count { get; private set; }

    public void AddFirst(T item)
    {
        var node = new DSNode<T>(item);

        if (_head is null)
        {
            _head = _tail = node;
            node.Next = node;
        }
        else
        {
            node.Next = _head;
            _head = node;
            _tail!.Next = _head;
        }

        Count++;
    }

    public void AddLast(T item)
    {
        var node = new DSNode<T>(item);

        if (_head is null)
        {
            _head = _tail = node;
            node.Next = node;
        }
        else
        {
            node.Next = _head;
            _tail!.Next = node;
            _tail = node;
        }

        Count++;
    }

    public T RemoveFirst()
    {
        if (_head is null)
            throw new InvalidOperationException(""The circular list is empty."");

        var value = _head.Value;

        if (Count == 1)
        {
            _head = _tail = null;
        }
        else
        {
            _head = _head.Next;
            _tail!.Next = _head;
        }

        Count--;
        return value;
    }

    public T RemoveLast()
    {
        if (_tail is null)
            throw new InvalidOperationException(""The circular list is empty."");

        var value = _tail.Value;

        if (Count == 1)
        {
            _head = _tail = null;
        }
        else
        {
            var current = _head!;

            while (current.Next != _tail)
                current = current.Next!;

            current.Next = _head;
            _tail = current;
        }

        Count--;
        return value;
    }

    public bool Contains(T item)
    {
        if (_head is null)
            return false;

        var comparer = EqualityComparer<T>.Default;
        var current = _head;

        do
        {
            if (comparer.Equals(current.Value, item))
                return true;

            current = current.Next!;
        }
        while (current != _head);

        return false;
    }

    public IEnumerable<T> Enumerate()
    {
        if (_head is null)
            yield break;

        var current = _head;

        do
        {
            yield return current.Value;
            current = current.Next!;
        }
        while (current != _head);
    }
}",
            [
                new("AddFirst","public void AddFirst(T item)","Insere um elemento no início e atualiza a referência de entrada.","O(1)"),
                new("AddLast","public void AddLast(T item)","Insere um elemento no final e atualiza a referência de saída.","O(1)"),
                new("RemoveFirst","public T RemoveFirst()","Remove o elemento da primeira posição e atualiza o início.","O(1)"),
                new("RemoveLast","public T RemoveLast()","Remove o elemento da última posição.","O(1) em lista duplamente ligada"),
                new("Contains","public bool Contains(T item)","Percorre ou consulta a estrutura para verificar a existência do valor.","O(n) em estruturas lineares"),
                new("Enumerate","public IEnumerable<T> Enumerate()","Percorre exatamente uma volta pelo ciclo, evitando repetição infinita.","O(n)"),
            ]),
        new("queue","linear","DSQueue","Fila","Estrutura FIFO: o primeiro elemento inserido é o primeiro removido.","Processamento por ordem de chegada e filas de tarefas.",@"namespace DataStructure.Abstractions;

public sealed class DSQueue<T>
{
    private DSNode<T>? _head;
    private DSNode<T>? _tail;

    public int Count { get; private set; }

    public void Enqueue(T item)
    {
        var node = new DSNode<T>(item);

        if (_tail is null)
        {

            _head = _tail = node;
        }
        else
        {

            _tail.Next = node;
            _tail = node;
        }

        Count++;
    }

    public T Dequeue()
    {
        if (_head is null)
            throw new InvalidOperationException(""The queue is empty."");

        var value = _head.Value;

        _head = _head.Next;

        if (_head is null)
            _tail = null;

        Count--;
        return value;
    }

    public T Peek()
    {
        if (_head is null)
            throw new InvalidOperationException(""The queue is empty."");

        return _head.Value;
    }
}",
            [
                new("Enqueue","public void Enqueue(T item)","Insere um item respeitando a extremidade de entrada da fila.","O(1)"),
                new("Dequeue","public T Dequeue()","Remove o próximo item segundo a política FIFO.","O(1)"),
                new("Peek","public T Peek()","Consulta o próximo item sem removê-lo.","O(1)"),
            ]),
        new("stack","linear","DSStack","Pilha","Estrutura LIFO: o último elemento inserido é o primeiro removido.","Desfazer ações, chamadas aninhadas e processamento reverso.",@"namespace DataStructure.Abstractions;

public sealed class DSStack<T>
{
    private DSNode<T>? _top;

    public int Count { get; private set; }

    public void Push(T item)
    {

        var node = new DSNode<T>(item)
        {
            Next = _top
        };

        _top = node;
        Count++;
    }

    public T Pop()
    {
        if (_top is null)
            throw new InvalidOperationException(""The stack is empty."");

        var value = _top.Value;

        _top = _top.Next;
        Count--;

        return value;
    }

    public T Peek()
    {
        if (_top is null)
            throw new InvalidOperationException(""The stack is empty."");

        return _top.Value;
    }
}",
            [
                new("Push","public void Push(T item)","Coloca um valor no topo da pilha.","O(1)"),
                new("Pop","public T Pop()","Remove e retorna o valor no topo.","O(1)"),
                new("Peek","public T Peek()","Consulta o próximo item sem removê-lo.","O(1)"),
            ]),
        new("deque","linear","DSDeque","Deque","Permite inserir, remover e consultar os dois extremos.","Quando ambos os lados precisam de operações eficientes.",@"namespace DataStructure.Abstractions;

public sealed class DSDeque<T>
{
    private DSNode<T>? _head;
    private DSNode<T>? _tail;

    public int Count { get; private set; }

    public void AddFirst(T item)
    {
        var node = new DSNode<T>(item);

        if (_head is null)
        {
            _head = _tail = node;
        }
        else
        {
            node.Next = _head;
            _head.Previous = node;
            _head = node;
        }

        Count++;
    }

    public void AddLast(T item)
    {
        var node = new DSNode<T>(item);

        if (_tail is null)
        {
            _head = _tail = node;
        }
        else
        {
            node.Previous = _tail;
            _tail.Next = node;
            _tail = node;
        }

        Count++;
    }

    public T RemoveFirst()
    {
        if (_head is null)
            throw new InvalidOperationException(""The deque is empty."");

        var value = _head.Value;
        _head = _head.Next;

        if (_head is null)
            _tail = null;
        else
            _head.Previous = null;

        Count--;
        return value;
    }

    public T RemoveLast()
    {
        if (_tail is null)
            throw new InvalidOperationException(""The deque is empty."");

        var value = _tail.Value;
        _tail = _tail.Previous;

        if (_tail is null)
            _head = null;
        else
            _tail.Next = null;

        Count--;
        return value;
    }

    public T PeekFirst()
    {
        if (_head is null)
            throw new InvalidOperationException(""The deque is empty."");

        return _head.Value;
    }

    public T PeekLast()
    {
        if (_tail is null)
            throw new InvalidOperationException(""The deque is empty."");

        return _tail.Value;
    }
}",
            [
                new("AddFirst","public void AddFirst(T item)","Insere um elemento no início e atualiza a referência de entrada.","O(1)"),
                new("AddLast","public void AddLast(T item)","Insere um elemento no final e atualiza a referência de saída.","O(1)"),
                new("RemoveFirst","public T RemoveFirst()","Remove o elemento da primeira posição e atualiza o início.","O(1)"),
                new("RemoveLast","public T RemoveLast()","Remove o elemento da última posição.","O(1) em lista duplamente ligada"),
                new("PeekFirst","public T PeekFirst()","Consulta o valor da primeira extremidade do deque.","O(1)"),
                new("PeekLast","public T PeekLast()","Consulta o valor da última extremidade do deque.","O(1)"),
            ]),
        new("deck","linear","DSDeck","Deque por nós","Variação de deque baseada diretamente em nós bidirecionais.","Como exemplo didático de manipulação de duas extremidades.",@"namespace DataStructure.Abstractions;

public sealed class DSDeck<T>
{
    private DSNode<T>? _head;
    private DSNode<T>? _tail;

    public int Count { get; private set; }

    public void AddFirst(T item)
    {
        var node = new DSNode<T>(item);

        if (_head is null)
            _head = _tail = node;
        else
        {
            node.Next = _head;
            _head.Previous = node;
            _head = node;
        }

        Count++;
    }

    public void AddLast(T item)
    {
        var node = new DSNode<T>(item);

        if (_tail is null)
            _head = _tail = node;
        else
        {
            _tail.Next = node;
            node.Previous = _tail;
            _tail = node;
        }

        Count++;
    }

    public T RemoveFirst()
    {
        if (_head is null)
            throw new InvalidOperationException(""The deck is empty."");

        var value = _head.Value;
        _head = _head.Next;

        if (_head is null)
            _tail = null;
        else
            _head.Previous = null;

        Count--;
        return value;
    }

    public T RemoveLast()
    {
        if (_tail is null)
            throw new InvalidOperationException(""The deck is empty."");

        var value = _tail.Value;
        _tail = _tail.Previous;

        if (_tail is null)
            _head = null;
        else
            _tail.Next = null;

        Count--;
        return value;
    }
}",
            [
                new("AddFirst","public void AddFirst(T item)","Insere um elemento no início e atualiza a referência de entrada.","O(1)"),
                new("AddLast","public void AddLast(T item)","Insere um elemento no final e atualiza a referência de saída.","O(1)"),
                new("RemoveFirst","public T RemoveFirst()","Remove o elemento da primeira posição e atualiza o início.","O(1)"),
                new("RemoveLast","public T RemoveLast()","Remove o elemento da última posição.","O(1) em lista duplamente ligada"),
            ]),
        new("priorityqueue","nonlinear","DSPriorityQueue","Fila de prioridade","Heap mínimo que sempre expõe o menor valor como próxima prioridade.","Escalonamento e processamento em ordem de prioridade.",@"namespace DataStructure.Abstractions;

public sealed class DSPriorityQueue<T> where T : IComparable<T>
{
    private readonly DSList<T> _heap = [];

    public int Count => _heap.Count;

    public void Enqueue(T item)
    {

        _heap.Add(item);

        SiftUp(_heap.Count - 1);
    }

    public T Peek()
    {
        if (_heap.Count == 0)
            throw new InvalidOperationException(""The priority queue is empty."");

        return _heap[0];
    }

    public T Dequeue()
    {
        if (_heap.Count == 0)
            throw new InvalidOperationException(""The priority queue is empty."");

        var result = _heap[0];
        var last = _heap[^1];

        _heap.RemoveAt(_heap.Count - 1);

        if (_heap.Count > 0)
        {

            _heap[0] = last;
            SiftDown(0);
        }

        return result;
    }

    private void SiftUp(int index)
    {
        while (index > 0)
        {

            var parent = (index - 1) / 2;

            if (_heap[parent].CompareTo(_heap[index]) <= 0)
                break;

            (_heap[parent], _heap[index]) =
                (_heap[index], _heap[parent]);

            index = parent;
        }
    }

    private void SiftDown(int index)
    {
        while (true)
        {
            var left = index * 2 + 1;
            var right = left + 1;
            var smallest = index;

            if (_heap.Count > left &&
                _heap[left].CompareTo(_heap[smallest]) < 0)
            {
                smallest = left;
            }

            if (_heap.Count > right &&
                _heap[right].CompareTo(_heap[smallest]) < 0)
            {
                smallest = right;
            }

            if (smallest == index)
                return;

            (_heap[index], _heap[smallest]) =
                (_heap[smallest], _heap[index]);

            index = smallest;
        }
    }
}",
            [
                new("Enqueue","public void Enqueue(T item)","Insere um item respeitando a extremidade de entrada da fila.","O(1)"),
                new("Peek","public T Peek()","Consulta o próximo item sem removê-lo.","O(1)"),
                new("Dequeue","public T Dequeue()","Remove o próximo item segundo a política FIFO.","O(1)"),
                new("SiftUp","private void SiftUp(int index)","Sobe um elemento enquanto ele viola a propriedade do heap com seu pai.","O(log n)"),
                new("SiftDown","private void SiftDown(int index)","Desce um elemento enquanto um dos filhos possui prioridade maior.","O(log n)"),
            ]),
        new("binarytree","nonlinear","DSBinaryTree","Árvore binária","Cada nó possui no máximo dois filhos e a estrutura oferece três percursos clássicos.","Representação hierárquica e estudo de percursos em árvores.",@"namespace DataStructure.Abstractions;

public sealed class DSBinaryTree<T>
{
    public DSBinaryTree(T value)
    {
        Root = new DSBinaryTreeNode<T>(value);
    }

    public DSBinaryTreeNode<T> Root { get; }

    public IEnumerable<T> PreOrder()
        => TraversePreOrder(Root);

    public IEnumerable<T> InOrder()
        => TraverseInOrder(Root);

    public IEnumerable<T> PostOrder()
        => TraversePostOrder(Root);

    private static IEnumerable<T> TraversePreOrder(DSBinaryTreeNode<T>? node)
    {
        if (node is null)
            yield break;

        yield return node.Value;

        foreach (var value in TraversePreOrder(node.Left))
            yield return value;

        foreach (var value in TraversePreOrder(node.Right))
            yield return value;
    }

    private static IEnumerable<T> TraverseInOrder(DSBinaryTreeNode<T>? node)
    {
        if (node is null)
            yield break;

        foreach (var value in TraverseInOrder(node.Left))
            yield return value;

        yield return node.Value;

        foreach (var value in TraverseInOrder(node.Right))
            yield return value;
    }

    private static IEnumerable<T> TraversePostOrder(DSBinaryTreeNode<T>? node)
    {
        if (node is null)
            yield break;

        foreach (var value in TraversePostOrder(node.Left))
            yield return value;

        foreach (var value in TraversePostOrder(node.Right))
            yield return value;

        yield return node.Value;
    }
}

public sealed class DSBinaryTreeNode<T>(T value)
{
    public T Value { get; set; } = value;
    public DSBinaryTreeNode<T>? Left { get; set; }
    public DSBinaryTreeNode<T>? Right { get; set; }
}",
            [
                new("Construtor","public DSArray(int length)","Aloca o armazenamento inicial da estrutura.","O(n)"),
                new("Root","public DSBinaryTreeNode<T> Root","Expõe o nó raiz a partir do qual a árvore é percorrida.","O(1)"),
                new("PreOrder","public IEnumerable<T> PreOrder()","Visita raiz, subárvore esquerda e subárvore direita.","O(n)"),
                new("InOrder","public IEnumerable<T> InOrder()","Visita esquerda, raiz e direita; em uma árvore de busca produz ordem crescente.","O(n)"),
                new("PostOrder","public IEnumerable<T> PostOrder()","Visita as duas subárvores antes da raiz.","O(n)"),
            ]),
        new("binarysearchtree","nonlinear","DSBinarySearchTree","Árvore binária de busca","Valores menores seguem para a esquerda e valores maiores ou iguais para a direita.","Busca ordenada, inserção hierárquica e estudo de árvores de busca.",@"namespace DataStructure.Abstractions;

public sealed class DSBinarySearchTree<T> where T : IComparable<T>
{
    public DSBinarySearchTreeNode<T>? Root { get; private set; }

    public int Count { get; private set; }

    public void Insert(T value)
    {
        if (Root is null)
        {
            Root = new DSBinarySearchTreeNode<T>(value);
            Count++;
            return;
        }

        var current = Root;

        while (true)
        {
            if (value.CompareTo(current.Value) < 0)
            {
                if (current.Left is null)
                {
                    current.Left = new DSBinarySearchTreeNode<T>(value);
                    Count++;
                    return;
                }

                current = current.Left;
            }
            else
            {
                if (current.Right is null)
                {
                    current.Right = new DSBinarySearchTreeNode<T>(value);
                    Count++;
                    return;
                }

                current = current.Right;
            }
        }
    }

    public bool Contains(T value)
    {
        var current = Root;

        while (current is not null)
        {
            var comparison = value.CompareTo(current.Value);

            if (comparison == 0)
                return true;

            current = comparison < 0
                ? current.Left
                : current.Right;
        }

        return false;
    }

    public IEnumerable<T> InOrder()
        => TraverseInOrder(Root);

    public IEnumerable<T> PreOrder()
        => TraversePreOrder(Root);

    public IEnumerable<T> PostOrder()
        => TraversePostOrder(Root);

    private static IEnumerable<T> TraverseInOrder(DSBinarySearchTreeNode<T>? node)
    {
        if (node is null)
            yield break;

        foreach (var value in TraverseInOrder(node.Left))
            yield return value;

        yield return node.Value;

        foreach (var value in TraverseInOrder(node.Right))
            yield return value;
    }

    private static IEnumerable<T> TraversePreOrder(DSBinarySearchTreeNode<T>? node)
    {
        if (node is null)
            yield break;

        yield return node.Value;

        foreach (var value in TraversePreOrder(node.Left))
            yield return value;

        foreach (var value in TraversePreOrder(node.Right))
            yield return value;
    }

    private static IEnumerable<T> TraversePostOrder(DSBinarySearchTreeNode<T>? node)
    {
        if (node is null)
            yield break;

        foreach (var value in TraversePostOrder(node.Left))
            yield return value;

        foreach (var value in TraversePostOrder(node.Right))
            yield return value;

        yield return node.Value;
    }
}

public sealed class DSBinarySearchTreeNode<T>(T value)
{
    public T Value { get; set; } = value;
    public DSBinarySearchTreeNode<T>? Left { get; set; }
    public DSBinarySearchTreeNode<T>? Right { get; set; }
}",
            [
                new("Insert","public void Insert(int index, T item)","Localiza a posição e conecta ou desloca os elementos necessários.","O(n)"),
                new("Contains","public bool Contains(T item)","Percorre ou consulta a estrutura para verificar a existência do valor.","O(n) em estruturas lineares"),
                new("InOrder","public IEnumerable<T> InOrder()","Visita esquerda, raiz e direita; em uma árvore de busca produz ordem crescente.","O(n)"),
                new("PreOrder","public IEnumerable<T> PreOrder()","Visita raiz, subárvore esquerda e subárvore direita.","O(n)"),
                new("PostOrder","public IEnumerable<T> PostOrder()","Visita as duas subárvores antes da raiz.","O(n)"),
            ]),
        new("heap","nonlinear","DSHeap","Heap mínimo","Árvore implícita em vetor onde o menor valor permanece na raiz.","Filas de prioridade e algoritmos que precisam do menor elemento repetidamente.",@"namespace DataStructure.Abstractions;

public sealed class DSHeap<T> where T : IComparable<T>
{
    private readonly List<T> _items = [];

    public int Count => _items.Count;

    public void Add(T value)
    {
        _items.Add(value);
        SiftUp(_items.Count - 1);
    }

    public T Peek()
        => _items.Count == 0
            ? throw new InvalidOperationException(""The heap is empty."")
            : _items[0];

    public T Remove()
    {
        if (_items.Count == 0)
            throw new InvalidOperationException(""The heap is empty."");

        var result = _items[0];
        var last = _items[^1];
        _items.RemoveAt(_items.Count - 1);

        if (_items.Count > 0)
        {
            _items[0] = last;
            SiftDown(0);
        }

        return result;
    }

    public IReadOnlyList<T> AsArray()
        => _items;

    private void SiftUp(int index)
    {
        while (index > 0)
        {
            var parent = (index - 1) / 2;

            if (_items[parent].CompareTo(_items[index]) <= 0)
                return;

            (_items[parent], _items[index]) =
                (_items[index], _items[parent]);

            index = parent;
        }
    }

    private void SiftDown(int index)
    {
        while (true)
        {
            var left = index * 2 + 1;
            var right = left + 1;
            var smallest = index;

            if (left < _items.Count &&
                _items[left].CompareTo(_items[smallest]) < 0)
                smallest = left;

            if (right < _items.Count &&
                _items[right].CompareTo(_items[smallest]) < 0)
                smallest = right;

            if (smallest == index)
                return;

            (_items[index], _items[smallest]) =
                (_items[smallest], _items[index]);

            index = smallest;
        }
    }
}",
            [
                new("Add","public void Add(T item)","Adiciona um valor ao final da estrutura.","O(1) amortizado"),
                new("Peek","public T Peek()","Consulta o próximo item sem removê-lo.","O(1)"),
                new("Remove","public bool Remove(T item)","Localiza o primeiro valor correspondente e o remove.","O(n)"),
                new("AsArray","public IReadOnlyList<T> AsArray()","Expõe a representação em vetor usada pelo heap.","O(1)"),
            ]),
        new("maxheap","nonlinear","DSMaxHeap","Max Heap","Heap binário máximo que mantém o maior elemento na raiz.","Quando é necessário obter repetidamente o maior valor com acesso à raiz em O(1), como em filas de prioridade máximas e seleção dos maiores elementos.",@"namespace DataStructure.Abstractions;

public sealed class DSMaxHeap<T> where T : IComparable<T>
{
    private readonly List<T> _items = [];

    public int Count => _items.Count;

    public void Add(T value)
    {
        _items.Add(value);
        SiftUp(_items.Count - 1);
    }

    public T Peek()
        => _items.Count == 0
            ? throw new InvalidOperationException(""The heap is empty."")
            : _items[0];

    public T Remove()
    {
        if (_items.Count == 0)
            throw new InvalidOperationException(""The heap is empty."");

        var result = _items[0];
        var last = _items[^1];
        _items.RemoveAt(_items.Count - 1);

        if (_items.Count > 0)
        {
            _items[0] = last;
            SiftDown(0);
        }

        return result;
    }

    public IReadOnlyList<T> AsArray()
        => _items;

    private void SiftUp(int index)
    {
        while (index > 0)
        {
            var parent = (index - 1) / 2;

            if (_items[parent].CompareTo(_items[index]) >= 0)
                return;

            (_items[parent], _items[index]) =
                (_items[index], _items[parent]);

            index = parent;
        }
    }

    private void SiftDown(int index)
    {
        while (true)
        {
            var left = index * 2 + 1;
            var right = left + 1;
            var largest = index;

            if (left < _items.Count &&
                _items[left].CompareTo(_items[largest]) > 0)
                largest = left;

            if (right < _items.Count &&
                _items[right].CompareTo(_items[largest]) > 0)
                largest = right;

            if (largest == index)
                return;

            (_items[index], _items[largest]) =
                (_items[largest], _items[index]);

            index = largest;
        }
    }
}",
            [
                new("Add","public void Add(T value)","Insere o valor no final da representação e sobe enquanto ele for maior que o pai.","O(log n)"),
                new("Peek","public T Peek()","Consulta o maior valor, armazenado na raiz.","O(1)"),
                new("Remove","public T Remove()","Remove a raiz, promove o último elemento e restaura a propriedade de max-heap.","O(log n)"),
                new("AsArray","public IReadOnlyList<T> AsArray()","Expõe a representação em níveis usada para visualizar o heap.","O(1)"),
            ]),
        new("hashtable","nonlinear","DSHashTable","Hash Table","Tabela de dispersão que associa chaves a valores por meio de buckets e tratamento de colisões por encadeamento.","Acesso médio constante a valores por chave, caches, índices, tabelas de símbolos e estruturas de associação chave/valor.",@"namespace DataStructure.Abstractions;

public sealed class DSHashTable<TKey, TValue> where TKey : notnull
{
    private const double MaxLoadFactor = 0.75;
    private List<Entry>[] _buckets;
    private int _count;

    public DSHashTable(int capacity = 8)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(capacity, 1);
        _buckets = CreateBuckets(capacity);
    }

    public int Count => _count;

    public TValue this[TKey key]
    {
        get
        {
            if (TryGetValue(key, out var value))
                return value;

            throw new KeyNotFoundException($""The key '{key}' was not found."");
        }
        set => Set(key, value);
    }

    public void Add(TKey key, TValue value)
    {
        var bucket = GetBucket(key);

        foreach (var entry in bucket)
        {
            if (EqualityComparer<TKey>.Default.Equals(entry.Key, key))
                throw new ArgumentException(""A value with the same key already exists."", nameof(key));
        }

        EnsureCapacity(_count + 1);
        GetBucket(key).Add(new Entry(key, value));
        _count++;
    }

    public void Set(TKey key, TValue value)
    {
        var bucket = GetBucket(key);

        for (var index = 0; index < bucket.Count; index++)
        {
            if (!EqualityComparer<TKey>.Default.Equals(bucket[index].Key, key))
                continue;

            bucket[index] = new Entry(key, value);
            return;
        }

        EnsureCapacity(_count + 1);
        GetBucket(key).Add(new Entry(key, value));
        _count++;
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        var bucket = GetBucket(key);

        foreach (var entry in bucket)
        {
            if (EqualityComparer<TKey>.Default.Equals(entry.Key, key))
            {
                value = entry.Value;
                return true;
            }
        }

        value = default!;
        return false;
    }

    public bool ContainsKey(TKey key)
        => TryGetValue(key, out _);

    public bool Remove(TKey key)
    {
        var bucket = GetBucket(key);

        for (var index = 0; index < bucket.Count; index++)
        {
            if (!EqualityComparer<TKey>.Default.Equals(bucket[index].Key, key))
                continue;

            bucket.RemoveAt(index);
            _count--;
            return true;
        }

        return false;
    }

    public void Clear()
    {
        _buckets = CreateBuckets(_buckets.Length);
        _count = 0;
    }

    public IEnumerable<KeyValuePair<TKey, TValue>> Enumerate()
    {
        foreach (var bucket in _buckets)
        {
            foreach (var entry in bucket)
                yield return new KeyValuePair<TKey, TValue>(entry.Key, entry.Value);
        }
    }

    private List<Entry> GetBucket(TKey key)
        => _buckets[GetBucketIndex(key, _buckets.Length)];

    private static int GetBucketIndex(TKey key, int bucketCount)
        => (key.GetHashCode() & 0x7fffffff) % bucketCount;

    private void EnsureCapacity(int required)
    {
        if (required <= _buckets.Length * MaxLoadFactor)
            return;

        var entries = Enumerate().ToArray();
        _buckets = CreateBuckets(_buckets.Length * 2);

        foreach (var entry in entries)
            GetBucket(entry.Key).Add(new Entry(entry.Key, entry.Value));
    }

    private static List<Entry>[] CreateBuckets(int count)
    {
        var buckets = new List<Entry>[count];

        for (var index = 0; index < count; index++)
            buckets[index] = [];

        return buckets;
    }

    private readonly record struct Entry(TKey Key, TValue Value);
}",
            [
                new("Construtor","public DSHashTable(int capacity = 8)","Cria os buckets vazios usados para distribuir as chaves.","O(m)"),
                new("Add","public void Add(TKey key, TValue value)","Calcula o bucket, procura chaves duplicadas e adiciona uma entrada; colisões permanecem encadeadas no mesmo bucket.","O(1) médio / O(n) pior caso"),
                new("Set","public void Set(TKey key, TValue value)","Procura a chave e atualiza seu valor; se não existir, adiciona uma nova entrada.","O(1) médio / O(n) pior caso"),
                new("TryGetValue","public bool TryGetValue(TKey key, out TValue value)","Calcula o bucket e percorre somente a cadeia daquele bucket até encontrar a chave.","O(1) médio / O(n) pior caso"),
                new("ContainsKey","public bool ContainsKey(TKey key)","Reutiliza TryGetValue para testar a existência da chave.","O(1) médio / O(n) pior caso"),
                new("Remove","public bool Remove(TKey key)","Calcula o bucket, percorre a cadeia e remove a entrada correspondente.","O(1) médio / O(n) pior caso"),
                new("Clear","public void Clear()","Descarta os buckets atuais e cria uma nova tabela vazia com a mesma capacidade.","O(m)"),
                new("Enumerate","public IEnumerable<KeyValuePair<TKey,TValue>> Enumerate()","Percorre bucket por bucket e depois cada entrada encadeada.","O(n)"),
            ]),
        new("weightedgraph","nonlinear","DSWeightedGraph","Grafo ponderado","Grafo direcionado em que cada aresta possui um custo não negativo.","Modelar rotas, redes e caminhos para Dijkstra, A* e busca gulosa.",@"namespace DataStructure.Abstractions;

public sealed class DSWeightedGraph<T> where T : notnull
{
    private readonly Dictionary<T, List<Edge>> _adjacency = [];

    public IReadOnlyCollection<T> Vertices => _adjacency.Keys;

    public void AddVertex(T vertex)
        => _adjacency.TryAdd(vertex, []);

    public void AddEdge(T from, T to, double weight)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(weight);
        AddVertex(from);
        AddVertex(to);

        var edges = _adjacency[from];

        for (var index = 0; index < edges.Count; index++)
        {
            if (!EqualityComparer<T>.Default.Equals(edges[index].To, to))
                continue;

            edges[index] = new Edge(to, weight);
            return;
        }

        edges.Add(new Edge(to, weight));
    }

    public IReadOnlyCollection<(T To, double Weight)> Neighbors(T vertex)
        => _adjacency.TryGetValue(vertex, out var edges)
            ? edges.Select(edge => (edge.To, edge.Weight)).ToArray()
            : [];

    private readonly record struct Edge(T To, double Weight);
}",
            [
                new("AddVertex","public void AddVertex(T vertex)","Garante a existência do vértice sem criar arestas.","O(1) amortizado"),
                new("AddEdge","public void AddEdge(T from, T to, double weight)","Cria uma aresta direcionada e registra seu peso não negativo.","O(1) amortizado"),
                new("Neighbors","public IReadOnlyCollection<(T To, double Weight)> Neighbors(T vertex)","Obtém as arestas de saída e seus custos.","O(1) para obter a coleção"),
                new("Vertices","public IReadOnlyCollection<T> Vertices","Expõe os vértices registrados no grafo.","O(1)")
            ]),
        new("graph","nonlinear","DSGraph","Grafo direcionado","Vértices conectados por arestas orientadas, representados por listas de adjacência.","Modelar relações, dependências, redes e caminhos.",@"namespace DataStructure.Abstractions;

public sealed class DSGraph<T> where T : notnull
{
    private readonly Dictionary<T, HashSet<T>> _adjacency = [];

    public IReadOnlyCollection<T> Vertices => _adjacency.Keys;

    public void AddVertex(T vertex)
        => _adjacency.TryAdd(vertex, []);

    public void AddEdge(T from, T to)
    {
        AddVertex(from);
        AddVertex(to);
        _adjacency[from].Add(to);
    }

    public bool HasEdge(T from, T to)
        => _adjacency.TryGetValue(from, out var neighbors)
            && neighbors.Contains(to);

    public IReadOnlyCollection<T> Neighbors(T vertex)
        => _adjacency.TryGetValue(vertex, out var neighbors)
            ? neighbors
            : [];

    public IEnumerable<T> BreadthFirst(T start)
    {
        if (!_adjacency.ContainsKey(start))
            yield break;

        var visited = new HashSet<T> { start };
        var queue = new Queue<T>();
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var vertex = queue.Dequeue();
            yield return vertex;

            foreach (var neighbor in _adjacency[vertex])
            {
                if (visited.Add(neighbor))
                    queue.Enqueue(neighbor);
            }
        }
    }

    public IEnumerable<T> DepthFirst(T start)
    {
        if (!_adjacency.ContainsKey(start))
            yield break;

        var visited = new HashSet<T>();

        foreach (var vertex in DepthFirstCore(start, visited))
            yield return vertex;
    }

    private IEnumerable<T> DepthFirstCore(T vertex, HashSet<T> visited)
    {
        if (!visited.Add(vertex))
            yield break;

        yield return vertex;

        foreach (var neighbor in _adjacency[vertex])
        {
            foreach (var next in DepthFirstCore(neighbor, visited))
                yield return next;
        }
    }
}",
            [
                new("AddVertex","public void AddVertex(T vertex)","Cria um vértice sem arestas caso ele ainda não exista.","O(1) amortizado"),
                new("AddEdge","public void AddEdge(T from, T to)","Garante os vértices e registra uma aresta direcionada.","O(1) amortizado"),
                new("HasEdge","public bool HasEdge(T from, T to)","Consulta se a lista de adjacência possui a conexão indicada.","O(1) médio"),
                new("Neighbors","public IReadOnlyCollection<T> Neighbors(T vertex)","Retorna os vizinhos diretamente conectados ao vértice.","O(1) para obter a coleção"),
                new("BreadthFirst","public IEnumerable<T> BreadthFirst(T start)","Usa uma fila e visita primeiro os vértices mais próximos da origem.","O(V + E)"),
                new("DepthFirst","public IEnumerable<T> DepthFirst(T start)","Explora cada caminho até o limite antes de voltar para outro ramo.","O(V + E)"),
            ]),
    ];

    public static IReadOnlyList<StructureGuide> Linear =>
        All.Where(item => item.Category == "linear").ToArray();

    public static IReadOnlyList<StructureGuide> NonLinear =>
        All.Where(item => item.Category == "nonlinear").ToArray();

    public static StructureGuide Get(string key) =>
        All.FirstOrDefault(item => item.Key.Equals(key, StringComparison.OrdinalIgnoreCase)) ?? All[0];

    public static string Normalize(string? key) => Get(key ?? All[0].Key).Key;
}
