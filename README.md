**Description:**  
A program to find paths between two nodes in an acyclic directed graph.  

**Usage:**  
**Graphs (<f,t>)... (L|R|LR) <F,T>**  
**f,t** represents a directed edge between positive integer nodes **f** and **t**.  
**F,T** are positive integer nodes between which the user wishes to search for a path.  
**R**, **L** or **LR** flags mark the desired direction of the path.  
Use **R** to search for paths from node **F** to **T**.  
Use **L** to search for paths from node **T** to **F**.  
Use **LR** to search for both.  

**Example:** 
**Graphs 1,2 1,3 3,4 3,5 LR 1,4 > output.txt**  
finds paths that go from node 1 to node 4 (**R** flag) and paths that go from node 4 to node 1 (**L** flag).  

**Example output:**  
Paths from 1 to 4:  
1->3->4  
Paths from 4 to 1:  
None  
