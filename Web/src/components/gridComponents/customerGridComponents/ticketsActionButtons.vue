<template>
  <div>
    <button
      class="btn btn-danger mr-2 mb-2 w-25"
      @click="
        deleteTicket = params.parent.getTicketId(params.data.id);
        params.parent.openDeleteModal();
      "
    >
      <font-awesome-icon icon="trash" />
    </button>
    <button
      class="btn btn-success w-50 mb-1"
      @click="
        clickedTicket = params.parent.getTicket(params.data);
        params.parent.openTicketDetailsModal();
        params.parent.loadTicketItems();
      "
    >
      <font-awesome-icon icon="info-circle" class="info-circle" />
    </button>
  </div>
</template>

<style scoped>
.info-circle {
  width: 25px;
  height: 25px;
}
</style>