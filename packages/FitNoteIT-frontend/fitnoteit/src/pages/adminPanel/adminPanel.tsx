import React from "react";
import "./adminPanel.scss";
import CustomPaginationActionsTable from "../../components/customPaginationActionsTable/customPaginationActionsTable";

export const AdminPanel: React.FC = () => {
  return (
    <div className="adminPanel">
      <CustomPaginationActionsTable />
    </div>
  );
};
