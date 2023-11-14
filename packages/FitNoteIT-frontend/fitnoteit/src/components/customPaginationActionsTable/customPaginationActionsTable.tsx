import * as React from "react";
import { useTheme } from "@mui/material/styles";
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
import { useEffect, useState } from "react";
import { AxiosResponse } from "axios";
import axiosInstance from "../../utils/axiosInstance";

const style = {
  position: "absolute" as "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 400,
  bgcolor: "background.paper",
  border: "2px solid #000",
  boxShadow: 24,
  p: 4,
};

interface TablePaginationActionsProps {
  count: number;
  page: number;
  rowsPerPage: number;
  onPageChange: (
    event: React.MouseEvent<HTMLButtonElement>,
    newPage: number,
  ) => void;
}

export const TablePaginationActions = (props: TablePaginationActionsProps) => {
  const theme = useTheme();
  const { count, page, rowsPerPage, onPageChange } = props;

  const handleFirstPageButtonClick = (
    event: React.MouseEvent<HTMLButtonElement>,
  ) => {
    onPageChange(event, 0);
  };

  const handleBackButtonClick = (
    event: React.MouseEvent<HTMLButtonElement>,
  ) => {
    onPageChange(event, page - 1);
  };

  const handleNextButtonClick = (
    event: React.MouseEvent<HTMLButtonElement>,
  ) => {
    onPageChange(event, page + 1);
  };

  const handleLastPageButtonClick = (
    event: React.MouseEvent<HTMLButtonElement>,
  ) => {
    onPageChange(event, Math.max(0, Math.ceil(count / rowsPerPage) - 1));
  };

  return (
    <Box sx={{ flexShrink: 0, ml: 2.5 }}>
      <IconButton
        onClick={handleFirstPageButtonClick}
        disabled={page === 0}
        aria-label="first page"
      >
        {theme.direction === "rtl" ? <LastPageIcon /> : <FirstPageIcon />}
      </IconButton>
      <IconButton
        onClick={handleBackButtonClick}
        disabled={page === 0}
        aria-label="previous page"
      >
        {theme.direction === "rtl" ? (
          <KeyboardArrowRight />
        ) : (
          <KeyboardArrowLeft />
        )}
      </IconButton>
      <IconButton
        onClick={handleNextButtonClick}
        disabled={page >= Math.ceil(count / rowsPerPage) - 1}
        aria-label="next page"
      >
        {theme.direction === "rtl" ? (
          <KeyboardArrowLeft />
        ) : (
          <KeyboardArrowRight />
        )}
      </IconButton>
      <IconButton
        onClick={handleLastPageButtonClick}
        disabled={page >= Math.ceil(count / rowsPerPage) - 1}
        aria-label="last page"
      >
        {theme.direction === "rtl" ? <FirstPageIcon /> : <LastPageIcon />}
      </IconButton>
    </Box>
  );
};

type RoleType = { name: string }[];
type User = {
  id?: string;
  userName: string;
  email: string;
  roles: RoleType;
};
export default function CustomPaginationActionsTable() {
  const [page, setPage] = useState<number>(0);
  const [rowsPerPage, setRowsPerPage] = useState<number>(5);
  const [open, setOpen] = useState<boolean>(false);
  const [selectedUser, setSelectedUser] = useState<User | null>(null);
  const [selectedRole, setSelectedRole] = useState<string>("");
  const [selectedRole2, setSelectedRole2] = useState<string>("");
  const [data, setData] = useState<User[]>([]);
  const [allRoles] = useState<string[]>(["Admin", "User"]);
  const [userRoles, setUserRoles] = useState<RoleType | any[]>([]);
  const currentUser = localStorage.getItem("currentUser");
  const token: string | null = localStorage.getItem("accessToken");
  const config2: { headers: { Authorization: string } } = {
    headers: { Authorization: `Bearer ${token}` },
  };

  const handleAddRole = async () => {
    if (selectedUser && selectedRole) {
      const userCopy: { roles: RoleType; userName: string; email: string } = {
        ...selectedUser,
      };
      const roleExists: boolean = userCopy.roles.some(
        (role: { name: string }): boolean => role.name === selectedRole,
      );

      if (!roleExists) {
        userCopy.roles.push({ name: selectedRole });
        setUserRoles(userCopy.roles);
        if (selectedUser.id) {
          let data = {
            id: selectedUser.id,
            roleName: selectedRole,
          };

          await axiosInstance
            .patch("/api/admin/role/grant", data, config2)
            .then((response: AxiosResponse<any, any>): void => {
              if (response.status == 200) {
                alert("Dodano role");
              } else {
                alert("Blad dodawania roli");
              }
            })
            .catch((error) => {
              console.log(error);
            });
        } else {
          console.log("selectedUser.id nie istnieje");
        }
      }
    }
    setSelectedRole("");
  };
  const handleDeleteRole = async () => {
    if (selectedUser && selectedRole2) {
      const userCopy: { roles: RoleType; userName: string; email: string } = {
        ...selectedUser,
      };
      const updatedRoles: { name: string }[] = userCopy.roles.filter(
        (role: { name: string }): boolean => role.name !== selectedRole2,
      );
      userCopy.roles = updatedRoles;

      if (selectedUser.id) {
        let data = {
          id: selectedUser.id,
          roleName: selectedRole2,
        };

        await axiosInstance
          .get(`/api/users/current`, config2)
          .then((response: AxiosResponse<any, any>): void => {
            if (response.status == 200) {
              if (
                response.data.roles.some(
                  (role: any) =>
                    role.name === "Admin" || role.name === "SuperAdmin",
                )
              ) {
                axiosInstance
                  .patch("/api/admin/role/remove", data, config2)
                  .then((response: AxiosResponse<any, any>): void => {
                    if (response.status == 200) {
                      alert("Usunieto role");
                    } else {
                      alert("Blad usuwania roli");
                    }
                  })
                  .catch((error) => console.log(error));
              } else {
                alert("Nie jestes adminem");
              }
            } else {
              alert("Blad usuwania roli");
            }
          })
          .catch((error) => console.log(error));
      } else {
        console.log("selectedUser.id nie istnieje");
      }

      const updatedData: (
        | { roles: RoleType; userName: string; email: string }
        | User
      )[] = data.map((user: User) => {
        if (user.userName === userCopy.userName) {
          return userCopy;
        }
        return user;
      });

      setUserRoles(updatedRoles);
      setSelectedUser(userCopy);
      setData(updatedData);
    }
    setSelectedRole2("");
  };
  const handleUserClick = (user: User): void => {
    setSelectedUser(user);
    setSelectedRole("");
    setUserRoles(user.roles);
    handleOpen();
  };

  useEffect(() => {
    const fetchData = async () => {
      let token: string | null = localStorage.getItem("accessToken");
      let config2: { headers: { Authorization: string } } = {
        headers: { Authorization: `Bearer ${token}` },
      };
      await axiosInstance
        .get("/api/admin/users", config2)
        .then((response: AxiosResponse<any, any>) => {
          setData(response.data);
        })
        .catch((error) => {
          console.error("Błąd podczas pobierania danych:", error);
        });
    };

    fetchData();
  }, []);

  const handleDeleteUser = async () => {
    if (selectedUser) {
      const updatedData: User[] = data.filter(
        (user: User): boolean => user.userName !== selectedUser.userName,
      );

      if (
        selectedUser.id &&
        !selectedUser.roles.some(
          (role: { name: string }) => role.name === "SuperAdmin",
        )
      ) {
        let data = {
          id: selectedUser.id,
        };

        await axiosInstance
          .delete(`/api/admin/users/${data.id}`, config2)
          .then((response: AxiosResponse<any, any>): void => {
            if (response.status == 204) {
              alert("usunieto uzytkownika");
            } else {
              alert("Blad usuwania uzytkownika");
            }
          })
          .catch((error) => {
            console.log(error);
          });
      } else {
        console.log("selectedUser.id nie istnieje lub jest SuperAdminem");
      }
      setData(updatedData);
      setSelectedUser(null);
      handleClose();
    }
  };

  const handleOpen = () => setOpen(true);
  const handleClose = () => {
    setOpen(false);
    setSelectedRole("");
  };

  const emptyRows =
    page > 0 ? Math.max(0, (1 + page) * rowsPerPage - data.length) : 0;

  const handleChangePage = (
    event: React.MouseEvent<HTMLButtonElement> | null,
    newPage: number,
  ) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (
    event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
  ) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  return (
    <>
      <TableContainer component={Paper}>
        <Table sx={{ minWidth: 500 }} aria-label="custom pagination table">
          <TableBody>
            <TableRow>
              <TableCell component="th" scope="row">
                <h4 className={"table-h4"}>Name</h4>
              </TableCell>
              <TableCell style={{ width: 160 }} align="right">
                <h4 className={"table-h4"}>Email</h4>
              </TableCell>
              <TableCell style={{ width: 160 }} align="right">
                <h4 className={"table-h4"}>Role</h4>
              </TableCell>
            </TableRow>
            {(rowsPerPage > 0
              ? data.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
              : data
            ).map((row) => (
              <TableRow
                key={row.userName}
                onClick={() => handleUserClick(row)}
                className={"table-user-row"}
              >
                <TableCell component="th" scope="row">
                  {row.userName}
                </TableCell>
                <TableCell style={{ width: 160 }} align="right">
                  {row.email}
                </TableCell>
                <TableCell style={{ width: 160 }} align="right">
                  {row.roles.map((role, index) => (
                    <span key={index}>
                      {role.name}
                      {index < row.roles.length - 1 && ", "}
                    </span>
                  ))}
                </TableCell>
              </TableRow>
            ))}
            {emptyRows > 0 && (
              <TableRow style={{ height: 53 * emptyRows }}>
                <TableCell colSpan={6} />
              </TableRow>
            )}
          </TableBody>
          <TableFooter>
            <TableRow>
              <TablePagination
                rowsPerPageOptions={[5, 10, 25, { label: "All", value: -1 }]}
                colSpan={3}
                count={data.length}
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
        <Box
          className={"custom-box"}
          sx={{
            ...style,
            backgroundColor: "#1e1d1d",
            color: "#b17a41",
            padding: "20px",
            textAlign: "center",
            borderRadius: "10px",
            borderColor: "#b17a41",
          }}
        >
          {selectedUser && (
            <div>
              <Typography
                id="modal-modal-title"
                variant="h4"
                component="h2"
                sx={{ color: "#b17a41" }}
              >
                User Information
              </Typography>
              <Typography
                id="modal-modal-description"
                sx={{ mt: 2, color: "white" }}
              >
                <strong>userName:</strong> {selectedUser.userName}
              </Typography>
              <Typography sx={{ mt: 1, color: "white" }}>
                <strong>Email:</strong> {selectedUser.email}
              </Typography>
              <Typography sx={{ mt: 1, color: "white" }}>
                <strong>Role:</strong>{" "}
                {userRoles.map((role) => role.name + " ")}
              </Typography>

              <div
                style={{
                  display: "flex",
                  flexDirection: "column",
                  alignItems: "center",
                }}
              >
                <p></p>
                <h2>Add role</h2>
                <select
                  value={selectedRole}
                  onChange={(e) => setSelectedRole(e.target.value)}
                  className="custom-select"
                >
                  <option value="">Select Role</option>
                  {allRoles.map((role) => (
                    <option key={role} value={role}>
                      {role}
                    </option>
                  ))}
                </select>
                <Button
                  className="button-role-confirm"
                  variant="contained"
                  color="primary"
                  disabled={!selectedRole}
                  onClick={handleAddRole}
                  sx={{
                    marginTop: "20px",
                    backgroundColor: "#b17a41",
                    color: "white",
                    fontSize: "16px",
                    padding: "10px 20px",
                    borderRadius: "5px",
                  }}
                >
                  Confirm Role Add
                </Button>
                <p></p>
                <h2>Delete role</h2>
                <select
                  value={selectedRole2}
                  onChange={(e) => setSelectedRole2(e.target.value)}
                  className="custom-select"
                >
                  <option value="">Select Role</option>
                  {userRoles
                    .map((role) => role.name)
                    .filter((role) => role !== "SuperAdmin")
                    .map((role) => (
                      <option key={role} value={role}>
                        {role}
                      </option>
                    ))}
                </select>
                <Button
                  className="button-role-confirm"
                  variant="contained"
                  color="primary"
                  disabled={!selectedRole2}
                  onClick={handleDeleteRole}
                  sx={{
                    marginTop: "20px",
                    backgroundColor: "#b17a41",
                    color: "white",
                    fontSize: "16px",
                    padding: "10px 20px",
                    borderRadius: "5px",
                  }}
                >
                  Confirm Role Delete
                </Button>
                <Button
                  variant="contained"
                  color="error"
                  onClick={handleDeleteUser}
                  sx={{
                    marginTop: "20px",
                    backgroundColor: "red",
                    color: "white",
                    fontSize: "16px",
                    padding: "10px",
                    borderRadius: "5px",
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
