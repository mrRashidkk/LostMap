import React, { useState, useEffect } from "react";
import { Dialog } from "@mui/material";
import api, { API_ROUTES } from "../../services/api";

const FindingDetailsDialog = ({ id, onClose }) => {
  const [finding, setFinding] = useState(null);

  useEffect(() => {
    const loadFinding = async () => {
      const url = `${API_ROUTES.getFinding}?id=${id}`;
      const response = await api.get(url);
      setFinding(response.data);
    };

    loadFinding();
  }, [id]);
  
  return (
    <Dialog open={true} onClose={onClose}>
      {!finding ? <div>Loading</div> :
        <>
          <div>{`Title: ${finding.title}`}</div>
          <div>{`Description: ${finding.description}`}</div>
          <div>{`Found: ${finding.found}`}</div>
        </>        
      }
    </Dialog>
  );
};

export default FindingDetailsDialog;