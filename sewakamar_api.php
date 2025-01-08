<?php
require_once 'database.php';
require_once 'SewaKamar.php';
$db = new MySQLDatabase();
$sewaKamar = new SewaKamar($db);

// Check the HTTP request method
$method = $_SERVER['REQUEST_METHOD'];

// Handle the different HTTP methods
switch ($method) {
    case 'GET':
        if (isset($_GET['id_sewa'])) {
            $id_sewa = $_GET['id_sewa'];
            $result = $sewaKamar->get_by_id($id_sewa);
        } else {
            $result = $sewaKamar->get_all();
        }

        $val = array();
        while ($row = $result->fetch_assoc()) {
            $val[] = $row;
        }

        header('Content-Type: application/json');
        echo json_encode($val);
        break;

    case 'POST':
        // Add a new sewa kamar
        $sewaKamar->id_kamar = $_POST['id_kamar'];
        $sewaKamar->id_penyewa = $_POST['id_penyewa'];
        $sewaKamar->tanggal_checkin = $_POST['tanggal_checkin'];
        $sewaKamar->tanggal_checkout = $_POST['tanggal_checkout'];
        $sewaKamar->jumlah_malam = $_POST['jumlah_malam'];
        $sewaKamar->total_biaya = $_POST['total_biaya'];

        $sewaKamar->insert();
        $a = $db->affected_rows();
        if ($a > 0) {
            $data['status'] = 'success';
            $data['message'] = 'Data Sewa Kamar created successfully.';
        } else {
            $data['status'] = 'failed';
            $data['message'] = 'Data Sewa Kamar not created.';
        }
        header('Content-Type: application/json');
        echo json_encode($data);
        break;

    case 'PUT':
        // Update an existing data
        $_PUT = [];
        parse_str(file_get_contents("php://input"), $_PUT);
        $sewaKamar->id_sewa = $_PUT['id_sewa'];
        $sewaKamar->id_kamar = $_PUT['id_kamar'];
        $sewaKamar->id_penyewa = $_PUT['id_penyewa'];
        $sewaKamar->tanggal_checkin = $_PUT['tanggal_checkin'];
        $sewaKamar->tanggal_checkout = $_PUT['tanggal_checkout'];
        $sewaKamar->jumlah_malam = $_PUT['jumlah_malam'];
        $sewaKamar->total_biaya = $_PUT['total_biaya'];

        $sewaKamar->update($sewaKamar->id_sewa);
        $a = $db->affected_rows();
        if ($a > 0) {
            $data['status'] = 'success';
            $data['message'] = 'Data Sewa Kamar updated successfully.';
        } else {
            $data['status'] = 'failed';
            $data['message'] = 'Data Sewa Kamar update failed.';
        }
        header('Content-Type: application/json');
        echo json_encode($data);
        break;

    case 'DELETE':
        // Delete a data
        if (isset($_GET['id_sewa'])) {
            $id_sewa = $_GET['id_sewa'];
            $sewaKamar->delete($id_sewa);
            $a = $db->affected_rows();
            if ($a > 0) {
                $data['status'] = 'success';
                $data['message'] = 'Data Sewa Kamar deleted successfully.';
            } else {
                $data['status'] = 'failed';
                $data['message'] = 'Data Sewa Kamar delete failed.';
            }
        } else {
            $data['status'] = 'failed';
            $data['message'] = 'Missing parameter: id_sewa';
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
