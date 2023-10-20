import * as React from "react";
import {useTheme} from "@mui/material/styles";
import Box from "@mui/material/Box";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableFooter from "@mui/material/TableFooter";
import TablePagination from "@mui/material/TablePagination";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import IconButton from "@mui/material/IconButton";
import FirstPageIcon from "@mui/icons-material/FirstPage";
import KeyboardArrowLeft from "@mui/icons-material/KeyboardArrowLeft";
import KeyboardArrowRight from "@mui/icons-material/KeyboardArrowRight";
import LastPageIcon from "@mui/icons-material/LastPage";
import "./customPaginationActionsTable.scss";
import Button from "@mui/material/Button";
import Modal from "@mui/material/Modal";
import Typography from "@mui/material/Typography";
import {useState} from "react";

const style = {
    position: 'absolute' as 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 400,
    bgcolor: 'background.paper',
    border: '2px solid #000',
    boxShadow: 24,
    p: 4,
};


interface TablePaginationActionsProps {
    count: number;
    page: number;
    rowsPerPage: number;
    onPageChange: (
        event: React.MouseEvent<HTMLButtonElement>,
        newPage: number
    ) => void;
}

export const TablePaginationActions = (props: TablePaginationActionsProps) => {


    const theme = useTheme();
    const {count, page, rowsPerPage, onPageChange} = props;

    const handleFirstPageButtonClick = (
        event: React.MouseEvent<HTMLButtonElement>
    ) => {
        onPageChange(event, 0);
    };

    const handleBackButtonClick = (
        event: React.MouseEvent<HTMLButtonElement>
    ) => {
        onPageChange(event, page - 1);
    };

    const handleNextButtonClick = (
        event: React.MouseEvent<HTMLButtonElement>
    ) => {
        onPageChange(event, page + 1);
    };

    const handleLastPageButtonClick = (
        event: React.MouseEvent<HTMLButtonElement>
    ) => {
        onPageChange(event, Math.max(0, Math.ceil(count / rowsPerPage) - 1));
    };

    return (
        <Box sx={{flexShrink: 0, ml: 2.5}}>
            <IconButton
                onClick={handleFirstPageButtonClick}
                disabled={page === 0}
                aria-label="first page"
            >
                {theme.direction === "rtl" ? <LastPageIcon/> : <FirstPageIcon/>}
            </IconButton>
            <IconButton
                onClick={handleBackButtonClick}
                disabled={page === 0}
                aria-label="previous page"
            >
                {theme.direction === "rtl" ? (
                    <KeyboardArrowRight/>
                ) : (
                    <KeyboardArrowLeft/>
                )}
            </IconButton>
            <IconButton
                onClick={handleNextButtonClick}
                disabled={page >= Math.ceil(count / rowsPerPage) - 1}
                aria-label="next page"
            >
                {theme.direction === "rtl" ? (
                    <KeyboardArrowLeft/>
                ) : (
                    <KeyboardArrowRight/>
                )}
            </IconButton>
            <IconButton
                onClick={handleLastPageButtonClick}
                disabled={page >= Math.ceil(count / rowsPerPage) - 1}
                aria-label="last page"
            >
                {theme.direction === "rtl" ? <FirstPageIcon/> : <LastPageIcon/>}
            </IconButton>
        </Box>
    );
};

function createData(name: string, email: string, role: string) {
    return {name, email, role};
}

const rows = [
    createData("Michal", "Michal@Michal.pl", "admin"),
    createData("Michal2", "Michal@uzytkownik.pl", "uzytkownik"),
    createData("Michal3", "uzytkownik@Michal.pl", "admin"),
    createData("Michal4", "uzytkownik@uzsytkownik.pl", "uzytkownik"),
    createData("Michal1", "Michal@Michadl.pl", "admin"),
    createData("Michal22", "Michal@uzytfkownik.pl", "uzytkownik"),
    createData("Michal33", "uzytkownik@Micshal.pl", "admin"),
    createData("Michal44", "uzytkownik@uzytdkownik.pl", "uzytkownik"),
    createData("Michal5", "Michal@Michasaal.pl", "admin"),
    createData("Michal26", "Michal@sad.pl", "uzytkownik"),
    createData("Michal37", "uzytkownik@asd.pl", "admin"),
    createData("Michal41", "asd@uzytkownik.pl", "uzytkownik"),

];
type User = {
    name: string;
    email: string;
    role: string;
};
export default function CustomPaginationActionsTable() {
    const [page, setPage] = useState<number>(0);
    const [rowsPerPage, setRowsPerPage] = useState<number>(5);
    const [open, setOpen] = useState<boolean>(false);
    const [selectedUser, setSelectedUser] = useState<User | null>(null);
    const [selectedRole, setSelectedRole] = useState<string>('');

    const handleUserClick = (user: User) => {
        setSelectedUser(user);
        setSelectedRole(user.role);
        handleOpen();
    };

    const handleDeleteUser = () => {

        setSelectedUser(null);
    };

    const handleRoleChange = (newRole: string) => {

        setSelectedUser(null);
    };

    const handleOpen = () => setOpen(true);
    const handleClose = () => {
        setOpen(false);
        setSelectedRole('');
    };


    const emptyRows =
        page > 0 ? Math.max(0, (1 + page) * rowsPerPage - rows.length) : 0;

    const handleChangePage = (
        event: React.MouseEvent<HTMLButtonElement> | null,
        newPage: number
    ) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (
        event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
    ) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };

    return (
        <>
            <TableContainer component={Paper}>
                <Table sx={{minWidth: 500}} aria-label="custom pagination table">
                    <TableBody>
                        <TableRow>
                            <TableCell component="th" scope="row">
                                <h4 className={"table-h4"}>Name</h4>
                            </TableCell>
                            <TableCell style={{width: 160}} align="right">
                                <h4 className={"table-h4"}>Email</h4>
                            </TableCell>
                            <TableCell style={{width: 160}} align="right">
                                <h4 className={"table-h4"}>Role</h4>
                            </TableCell>
                        </TableRow>
                        {(rowsPerPage > 0
                                ? rows.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                                : rows
                        ).map((row) => (
                            <TableRow key={row.name} onClick={() => handleUserClick(row)} className={"table-user-row"}>
                                <TableCell component="th" scope="row">
                                    {row.name}
                                </TableCell>
                                <TableCell style={{width: 160}} align="right">
                                    {row.email}
                                </TableCell>
                                <TableCell style={{width: 160}} align="right">
                                    {row.role}
                                </TableCell>
                            </TableRow>
                        ))}
                        {emptyRows > 0 && (
                            <TableRow style={{height: 53 * emptyRows}}>
                                <TableCell colSpan={6}/>
                            </TableRow>
                        )}
                    </TableBody>
                    <TableFooter>
                        <TableRow>
                            <TablePagination
                                rowsPerPageOptions={[5, 10, 25, {label: "All", value: -1}]}
                                colSpan={3}
                                count={rows.length}
                                rowsPerPage={rowsPerPage}
                                page={page}
                                SelectProps={{
                                    inputProps: {
                                        "aria-label": "rows per page",
                                    },
                                    native: true,
                                }}
                                onPageChange={handleChangePage}
                                onRowsPerPageChange={handleChangeRowsPerPage}
                                ActionsComponent={TablePaginationActions}
                            />
                        </TableRow>
                    </TableFooter>
                </Table>
            </TableContainer>

            <Modal
                open={open}
                onClose={handleClose}
                aria-labelledby="modal-modal-title"
                aria-describedby="modal-modal-description"
                className="custom-modal"
            >
                <Box className={"custom-box"} sx={{
                    ...style,
                    backgroundColor: '#1e1d1d',
                    color: '#b17a41',
                    padding: '20px',
                    textAlign: 'center',
                    borderRadius: '10px',
                    borderColor: "#b17a41"
                }}>
                    {selectedUser && (
                        <div>
                            <Typography id="modal-modal-title" variant="h4" component="h2" sx={{color: '#b17a41'}}>
                                User Information
                            </Typography>
                            <Typography id="modal-modal-description" sx={{mt: 2, color: 'white'}}>
                                <strong>Name:</strong> {selectedUser.name}
                            </Typography>
                            <Typography sx={{mt: 1, color: 'white'}}>
                                <strong>Email:</strong> {selectedUser.email}
                            </Typography>
                            <Typography sx={{mt: 1, color: 'white'}}>
                                <strong>Role:</strong> {selectedUser.role}
                            </Typography>
                            <div style={{display: 'flex', flexDirection: 'column', alignItems: 'center'}}>
                                <select
                                    value={selectedRole}
                                    onChange={(e) => setSelectedRole(e.target.value)}
                                    className="custom-select"
                                >
                                    <option value="">Select Role</option>
                                    <option value="admin">Admin</option>
                                    <option value="user">User</option>
                                </select>
                                <Button className="button-role-confirm"
                                        variant="contained"
                                        color="primary"
                                        disabled={!selectedRole}
                                        onClick={() => {
                                            handleRoleChange(selectedRole);
                                            setSelectedRole('');
                                        }}
                                        sx={{
                                            marginTop: '20px',
                                            backgroundColor: '#b17a41',
                                            color: 'white',
                                            fontSize: '16px',
                                            padding: '10px 20px',
                                            borderRadius: '5px',
                                        }}
                                >
                                    Confirm Role Change
                                </Button>
                                <Button
                                    variant="contained"
                                    color="error"
                                    onClick={handleDeleteUser}
                                    sx={{
                                        marginTop: '20px',
                                        backgroundColor: 'red',
                                        color: 'white',
                                        fontSize: '16px',
                                        padding: '10px',
                                        borderRadius: '5px',
                                    }}
                                >
                                    Delete User
                                </Button>
                            </div>
                        </div>
                    )}
                </Box>
            </Modal>
        </>
    );
}


