<template>
  <div>
    <button
      class="btn btn-success mr-2 mb-3 w-25 pl-3 pr-3"
      @click="
        updateCustomer = params.parent.getCustomer(params.data);
        params.parent.openUpdateModal();
      "
    >
      <font-awesome-icon icon="edit" />
    </button>
    <button
      class="btn btn-danger mr-2 mb-3 w-25 pl-3 pr-3"
      @click="
        deletingCustomerId = params.parent.getCustomerId(params.data.id);
        params.parent.openDeleteModal();
      "
    >
      <font-awesome-icon icon="trash" />
    </button>
    <router-link :to="`/assets/${params.data.id}`">
      <button class="btn btn-success mb-3">Assets</button></router-link
    >
  </div>
</template>

