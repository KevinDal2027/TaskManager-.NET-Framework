const API_URL = "http://localhost:5075/tasks"; // Change this if needed

document.addEventListener("DOMContentLoaded", () => {
    const taskForm = document.getElementById("task-form");
    const taskName = document.getElementById("task-name");
    const taskCategory = document.getElementById("task-category");
    const taskPriority = document.getElementById("task-priority");
    const taskDueDate = document.getElementById("task-due-date");
    const taskList = document.getElementById("task-list");

    // Fetch and display tasks
    async function fetchTasks() {
        taskList.innerHTML = "";
        try {
            const response = await fetch(API_URL);
            let tasks = await response.json();

            // Sort by due date first, then by priority (higher number = higher priority)
            tasks.sort((a, b) => {
                const dateA = new Date(a.dueDate);
                const dateB = new Date(b.dueDate);
                if (dateA - dateB !== 0) {
                    return dateA - dateB; // Sort by due date (earliest first)
                }
                return b.priority - a.priority; // If same date, sort by priority (higher first)
            });

            tasks.forEach(task => addTaskToUI(task));
        } catch (error) {
            console.error("Error fetching tasks:", error);
        }
    }

    // Add a task to UI
    function addTaskToUI(task) {
        const taskItem = document.createElement("li");
        taskItem.classList.add("task");
        taskItem.dataset.id = task.id;
        taskItem.innerHTML = `
            <strong>${task.name}</strong>
            <span class="category">${task.categoryId === 1 ? "Academics" : "Others"}</span><br>
            <span class="priority">Priority: ${task.priority}</span><br>
            <span class="due-date">Due Date: ${task.dueDate}</span>
            <div class="buttons">
                <button class="edit" onclick="editTask(${task.id})">Edit</button>
                <button onclick="deleteTask(${task.id})">Delete</button>
            </div>
        `;

        taskList.appendChild(taskItem);
    }

    // Handle form submission (Add Task)
    taskForm.addEventListener("submit", async (e) => {
        e.preventDefault();

        const newTask = {
            name: taskName.value.trim(),
            categoryId: Number(taskCategory.value),
            priority: Number(taskPriority.value),
            dueDate: taskDueDate.value
        };

        try {
            const response = await fetch(API_URL, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(newTask)
            });

            if (response.ok) {
                fetchTasks();
            }
        } catch (error) {
            console.error("Error adding task:", error);
        }

        // Clear form
        taskName.value = "";
        taskCategory.value = "1";
        taskPriority.value = "";
        taskDueDate.value = "";
    });

    // Edit a task
    async function editTask(id) {
        const newName = prompt("Enter new task name:");
        const newPriority = prompt("Enter new priority (1-100):");
        const newDueDate = prompt("Enter new due date (YYYY-MM-DD):");

        if (!newName || !newPriority || !newDueDate) return;

        const updatedTask = {
            name: newName,
            priority: Number(newPriority),
            dueDate: newDueDate
        };

        try {
            const response = await fetch(`${API_URL}/${id}`, {
                method: "PUT",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(updatedTask)
            });

            if (response.ok) {
                fetchTasks();
            }
        } catch (error) {
            console.error("Error updating task:", error);
        }
    }

    // Delete a task
    async function deleteTask(id) {
        if (!confirm("Are you sure you want to delete this task?")) return;

        try {
            const response = await fetch(`${API_URL}/${id}`, { method: "DELETE" });

            if (response.ok) {
                fetchTasks();
            }
        } catch (error) {
            console.error("Error deleting task:", error);
        }
    }

    fetchTasks(); // Load tasks on page load
});
