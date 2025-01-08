<?php
require_once 'database.php';
require_once 'User.php';
$db = new MySQLDatabase();
$user = new User($db);

// Check the HTTP request method
$method = $_SERVER['REQUEST_METHOD'];

// Handle the different HTTP methods
switch ($method) {
    case 'GET':
        if (isset($_GET['id'])) {
            $id = $_GET['id'];
        }
        if (isset($_GET['username'])) {
            $username = $_GET['username'];
        }
        if ($id > 0) {
            $result = $user->get_by_id($id);
        } elseif ($username != "") {
            $result = $user->get_by_kodemk($username);
        } else {
            $result = $user->get_all();
        }

        $val = array();
        while ($row = $result->fetch_assoc()) {
            $val[] = $row;
        }

        header('Content-Type: application/json');
        echo json_encode($val);
        break;

    case 'POST':
        // Add a new user
        $user->username = $_POST['username'];
        $user->passwd = $_POST['passwd'];
        $user->rolename = $_POST['rolename'];

        $user->insert();
        $a = $db->affected_rows();
        if ($a > 0) {
            $data['status'] = 'success';
            $data['message'] = 'Data User created successfully.';
        } else {
            $data['status'] = 'failed';
            $data['message'] = 'Data User not created.';
        }
        header('Content-Type: application/json');
        echo json_encode($data);
        break;

    case 'PUT':
        // Update an existing data
        $_PUT = [];
        parse_str(file_get_contents("php://input"), $_PUT);
        $user->username = $_PUT['username'];
        $user->passwd = $_PUT['passwd'];
        $user->rolename = $_PUT['rolename'];

        $a = $user->update_by_kodemk($user->username);
        if ($a > 0) {
            $data['status'] = 'success';
            $data['message'] = 'Data User updated successfully.';
        } else {
            $data['status'] = 'failed';
            $data['message'] = 'Data User update failed.';
        }
        header('Content-Type: application/json');
        echo json_encode($data);
        break;

    case 'DELETE':
        // Delete a user
        if (isset($_GET['id'])) {
            $id = $_GET['id'];
        }
        if (isset($_GET['username'])) {
            $username = $_GET['username'];
        }
        if ($id > 0) {
            $user->delete($id);
        } elseif ($username != "") {
            $user->delete_by_kodemk($username);
        } else {
            // Handle delete request without id or username
        }

        $a = $db->affected_rows();
        if ($a > 0) {
            $data['status'] = 'success';
            $data['message'] = 'Data User deleted successfully.';
        } else {
            $data['status'] = 'failed';
            $data['message'] = 'Data User delete failed.';
        }
        header('Content-Type: application/json');
        echo json_encode($data);
        break;

    default:
        header("HTTP/1.0 405 Method Not Allowed");
        break;
}

$db->close();
?>
